# SmartTenderWindow

A C# Windows Forms class library that provides a modal payment-tender allocation dialog. It lets users split a document total across multiple payment methods (cash, card, check, etc.), enforcing per-tender caps and computing change due. It is designed to integrate with the **SmartFramework.Sage50c** ecosystem.

---

## File & Folder Structure

```
smart-tender-window/
├── Properties/
│   └── AssemblyInfo.cs               # Assembly metadata (v1.0.0.0)
├── Forms/
│   ├── TenderSplitDialog.cs          # Main dialog form – logic & event handling
│   ├── TenderSplitDialog.Designer.cs # Designer-generated UI layout
│   └── TenderSplitDialog.resx        # Embedded form resources
├── Models/
│   ├── TenderItem.cs                 # Represents one payment method
│   ├── TenderAllocation.cs           # Amount allocated to a single tender
│   └── TenderSplitResult.cs          # Full result returned on confirmation
├── SmartTenderWindow.csproj          # Project file (.NET Framework 4.7.2)
├── SmartTenderWindow.slnx            # Solution configuration
├── bin/Debug/                        # Compiled output
└── obj/                              # Build intermediates
```

---

## Main Modules / Classes / Functions

### `Forms/TenderSplitDialog.cs`

The core Windows Form. Dynamically builds one input row per tender and validates allocations in real time.

#### Constructor

```csharp
TenderSplitDialog(IEnumerable<TenderItem> tenders, decimal documentTotal)
```

- Validates that `tenders` is non-null and `documentTotal` is positive.
- Calls `BuildTenderRows()` and `AdjustFormHeight()` to lay out the dynamic UI.

#### Static convenience method

```csharp
TenderSplitResult? Show(IWin32Window owner, IEnumerable<TenderItem> tenders,
                         decimal documentTotal, string title)
```

Shows the dialog modally. Returns a `TenderSplitResult` on confirmation, or `null` on cancel.

#### Key private methods

| Method | Description |
|---|---|
| `BuildTenderRows()` | Creates a `Label` + `NumericUpDown` pair for every `TenderItem`. |
| `AdjustFormHeight()` | Sizes the form based on tender count. |
| `OnAmountChanged()` | Validates each input: enforces `MaxAmount` and caps non-change tenders at the document total. |
| `UpdateSummary()` | Recalculates allocated, remaining, and change-due amounts and refreshes the summary panel. |
| `GetValidationError()` | Returns a localised error string, or `null` when input is valid. Rules: at least one value must be entered; total must not be less than document total; non-change tenders may not individually exceed document total. |
| `SetWithoutEvent()` | Updates a `NumericUpDown` without triggering the `ValueChanged` handler. |
| `FormatCurrency()` | Formats a `decimal` as `"#,##0.00 €"`. |

#### UI layout

```
┌─────────────────────────────────┐
│  [Header] Document total        │  Dark-blue panel, 16pt bold
├─────────────────────────────────┤
│  Meio de Pagamento  │  Valor    │  Column headers
├─────────────────────────────────┤
│  [Label]            [NumUpDown] │  ← one row per TenderItem
│  …                              │
├─────────────────────────────────┤
│  Total Alocado:  0,00 €         │
│  Por Alocar:     0,00 €         │  Summary panel
│  Troco:          0,00 €         │
│  [validation message]           │
├─────────────────────────────────┤
│       [Confirmar]  [Cancelar]   │
└─────────────────────────────────┘
```

---

### `Models/TenderItem.cs`

Represents one available payment method.

```csharp
public class TenderItem
{
    public string Id { get; }              // Unique identifier (non-empty)
    public string Name { get; }            // Display name, e.g. "Numerário"
    public decimal PreloadedAmount { get; set; }  // Default starting value
    public decimal? MaxAmount { get; set; }       // Optional hard cap
    public bool AllowsChange { get; set; }        // true = overpayment → change; false = capped at total
}
```

Constructor throws `ArgumentException` when `Id` or `Name` is null/empty.

---

### `Models/TenderAllocation.cs`

Pairs a `TenderItem` with the amount the user entered.

```csharp
public class TenderAllocation
{
    public TenderItem Tender { get; }
    public decimal Amount { get; }
}
```

---

### `Models/TenderSplitResult.cs`

The value returned by `TenderSplitDialog.Show()`.

```csharp
public class TenderSplitResult
{
    public IReadOnlyList<TenderAllocation> Allocations { get; }  // Amount > 0 only
    public decimal TotalAllocated { get; }
    public decimal ChangeDue { get; }                            // 0 when no overpayment
}
```

---

## Dependencies

| Dependency | Source |
|---|---|
| `System.Windows.Forms` | .NET Framework 4.7.2 |
| `System.Drawing` | .NET Framework 4.7.2 |
| `System.Core` / `System.Xml.Linq` | .NET Framework 4.7.2 |
| `Microsoft.CSharp` | COM interop support |

No third-party NuGet packages are required.

---

## Usage Instructions

### 1. Build

```powershell
# Debug build (outputs to bin\Debug\)
msbuild SmartTenderWindow.csproj /p:Configuration=Debug /p:Platform=AnyCPU

# Release build
msbuild SmartTenderWindow.csproj /p:Configuration=Release /p:Platform=AnyCPU
```

### 2. Reference the DLL

Add a reference to `smart-tender-window.dll` in your consuming project, or add the `.csproj` to your solution.

### 3. Show the dialog

```csharp
using SmartTenderWindowTenderSplit.Forms;
using SmartTenderWindowTenderSplit.Models;

var tenders = new List<TenderItem>
{
    new TenderItem("CASH", "Numerário")   { AllowsChange = true,  PreloadedAmount = 50m },
    new TenderItem("CARD", "Cartão")      { AllowsChange = false, MaxAmount = 500m },
    new TenderItem("CHECK", "Cheque")     { AllowsChange = false },
};

decimal documentTotal = 37.50m;

TenderSplitResult? result = TenderSplitDialog.Show(
    owner: this,
    tenders: tenders,
    documentTotal: documentTotal,
    title: "Pagamento"
);

if (result != null)
{
    foreach (var alloc in result.Allocations)
        Console.WriteLine($"{alloc.Tender.Name}: {alloc.Amount:N2} €");

    Console.WriteLine($"Troco: {result.ChangeDue:N2} €");
}
```

### 4. Validation rules enforced automatically

- At least one tender amount must be greater than zero.
- `TotalAllocated` must be ≥ `documentTotal`.
- For tenders where `AllowsChange = false`, the entered amount may not exceed `documentTotal`.
- If a `TenderItem.MaxAmount` is set, the input is capped at that value.

---

## Integration with SmartFramework.Sage50c

This library is designed to complement the **SmartFramework.Sage50c** ERP integration framework. Typical usage flow:

1. `SmartApp` (host application) opens a POS transaction via `DocumentController` / `TenderTransactionController`.
2. The host calls `TenderSplitDialog.Show()` to let the user allocate payment.
3. The returned `TenderSplitResult.Allocations` are applied to the Sage 50c transaction using `TenderLineItemsUtility`.
4. The transaction is committed through the Sage COM API.

---

## Namespaces

| Namespace | Contents |
|---|---|
| `SmartTenderWindowTenderSplit.Forms` | `TenderSplitDialog` |
| `SmartTenderWindowTenderSplit.Models` | `TenderItem`, `TenderAllocation`, `TenderSplitResult` |
