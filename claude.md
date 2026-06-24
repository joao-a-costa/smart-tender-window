# SmartTenderWindow

A C# Windows Forms class library that provides a modal POS-style payment-tender allocation dialog. It lets users split a document total across multiple payment methods (cash, card, check, etc.), enforcing per-tender caps and computing change due. It is designed to integrate with the **SmartFramework.Sage50c** ecosystem.

---

## File & Folder Structure

```
smart-tender-window/
├── SmartTenderWindow/                        # Class library (DLL)
│   ├── Properties/
│   │   └── AssemblyInfo.cs
│   ├── Forms/
│   │   ├── TenderSplitDialog.cs          # Main dialog – logic & event handling
│   │   ├── TenderSplitDialog.Designer.cs # Designer-generated UI layout
│   │   └── TenderSplitDialog.resx
│   ├── Models/
│   │   ├── TenderItem.cs                 # Represents one payment method
│   │   ├── TenderAllocation.cs           # Amount allocated to a single tender
│   │   └── TenderSplitResult.cs          # Full result returned on confirmation
│   └── SmartTenderWindow.csproj          # .NET Framework 4.7.2, old-style csproj
│
├── SmartTenderWindow.Windows/                # WinForms test harness (WinExe)
│   ├── MainForm.cs                       # UI to pick tender count & document total
│   ├── MainForm.Designer.cs
│   ├── Program.cs
│   └── SmartTenderWindow.Windows.csproj
│
├── SmartTenderWindow.Tests/                  # MSTest unit test project
│   ├── Helpers/
│   │   ├── StaHelper.cs                  # Runs actions on STA thread (required for WinForms)
│   │   └── ReflectionHelper.cs           # Invokes private methods/fields by name
│   ├── Models/
│   │   ├── TenderItemTests.cs            # 14 tests
│   │   ├── TenderAllocationTests.cs      # 5 tests
│   │   └── TenderSplitResultTests.cs     # 5 tests
│   ├── Forms/
│   │   └── TenderSplitDialogTests.cs     # 32 tests
│   └── SmartTenderWindow.Tests.csproj    # SDK-style, net472, MSTest 3.1.1
│
├── SmartTenderWindow.slnx                    # Solution (all three projects)
├── bin/Debug/
└── obj/
```

---

## Main Modules / Classes / Functions

### `Forms/TenderSplitDialog.cs`

The core Windows Form. POS cash-register style: full-screen maximized dialog with a scrollable tender list, numpad, and live summary panel.

#### Constructor

```csharp
TenderSplitDialog(IEnumerable<TenderItem> tenders, decimal documentTotal)
```

- Throws `ArgumentNullException` when `tenders` is null.
- Throws `ArgumentException` when `tenders` is empty.
- Throws `ArgumentOutOfRangeException` when `documentTotal` ≤ 0.
- Preloads `_amounts[]` from each `TenderItem.PreloadedAmount`.
- Calls `BuildTenderRows()` then `SelectTender(0)`.

#### Static convenience method

```csharp
TenderSplitResult Show(IWin32Window owner, IEnumerable<TenderItem> tenders,
                       decimal documentTotal, string title = null)
```

Shows the dialog modally. Returns a `TenderSplitResult` on confirmation, or `null` on cancel.

#### Key private methods

| Method | Description |
|---|---|
| `BuildTenderRows()` | Dynamically creates one `Panel` row (name label + amount label) per `TenderItem`. Hooks `ClientSizeChanged` to keep row widths in sync when the scrollbar appears/disappears. |
| `SelectTender(int index)` | Highlights the selected row (green), syncs `_inputBuffer` to the current amount, scrolls the row into view. No-op when index equals `_selectedIndex`. |
| `NavigateTender(int delta)` | Moves selection by `delta`, clamped to `[0, count-1]`. |
| `HandleNumpad(string key)` | Cash-register input: digits append right, `"⌫"` removes last digit (floors to `"0"`), `"."` is a no-op (2 decimal places are implied). Calls `CommitBuffer()`. |
| `CommitBuffer()` | Parses `_inputBuffer` as `raw / 100m`. Enforces `MaxAmount` cap, then enforces `documentTotal` cap for `AllowsChange = false` tenders. |
| `UpdateSummary()` | Recalculates delivered / missing. Shows **"Em falta:"** (red) when underpaid, **"Troco:"** (green) when overpaid or exact. Enables/disables `btnConfirm`. |
| `Confirm()` | Guards against underpayment. Builds `TenderSplitResult` (only allocations with Amount > 0), sets `DialogResult = OK`. |
| `FormatCurrency(decimal)` | Returns `value.ToString("N2") + " €"`. |
| `AmountToBuffer(decimal)` | Returns `((long)(value * 100)).ToString()`, or `"0"` for zero. |

#### UI layout

```
┌────────────────────────────────────────────────────────────┐
│  [Green header]  Title               Total: 37,50 €        │  52px, DockStyle.Top
├──────────────────────────────────────────┬─────────────────┤
│  [Scrollable tender list]                │  [7] [8] [9]    │
│   Numerário              0,00 €  ◄ sel  │  [4] [5] [6]    │
│   Cartão                 0,00 €          │  [1] [2] [3]    │
│   …                                      │  [⌫] [0] [.]    │
│                                          │  [    OK    ]   │
│  Valor entregue:  0,00 €                 │                 │
│  Em falta:       37,50 €                 │                 │  370px numpad, DockStyle.Right
│  Total:          37,50 €                 │                 │
│  [▲] [▼]   [Confirmar (gray)]  [Cancelar]│                 │
└──────────────────────────────────────────┴─────────────────┘
```

- `FormBorderStyle.Sizable`, `WindowState.Maximized`, `KeyPreview = true`
- Keyboard: 0–9, Backspace/Delete → numpad; ↑/↓ → navigate; Enter → confirm; Escape → cancel

---

### `Models/TenderItem.cs`

```csharp
public class TenderItem
{
    public string Id { get; set; }
    public string Name { get; set; }
    public decimal PreloadedAmount { get; set; }   // Default starting value (default 0)
    public decimal? MaxAmount { get; set; }         // Optional hard cap (default null)
    public bool AllowsChange { get; set; }          // true = overpayment → change; false = capped at total
}
```

Two-arg constructor throws `ArgumentException` when `Id` or `Name` is null/empty/whitespace. Default (no-arg) constructor also available.

---

### `Models/TenderAllocation.cs`

```csharp
public class TenderAllocation
{
    public TenderItem Tender { get; set; }
    public decimal Amount { get; set; }
}
```

---

### `Models/TenderSplitResult.cs`

```csharp
public class TenderSplitResult
{
    public List<TenderAllocation> Allocations { get; set; }  // Only entries with Amount > 0
    public decimal TotalAllocated { get; set; }
    public decimal ChangeDue { get; set; }                   // 0 when no overpayment
}
```

---

### `SmartTenderWindow.Windows/MainForm.cs`

Test harness. On startup randomises tender count (1–10) and document total (0.01–500.00). Button click picks random names from a pool of 10; falls back to `"Tender {i+1}"` when the pool is exhausted (supports up to 100 tenders). First tender always gets `AllowsChange = true`.

---

### `SmartTenderWindow.Tests`

MSTest 3.1.1 project targeting `net472`. Uses two helpers:

- **`StaHelper.Run(Action)`** — wraps any action in a dedicated STA thread (required because WinForms controls must be created on STA). Rethrows exceptions with original stack trace via `ExceptionDispatchInfo`.
- **`ReflectionHelper`** — `InvokePrivate`, `InvokePrivateStatic`, `GetField<T>`, `SetField` — gives tests access to private methods and fields without modifying production code.

| Test class | Tests | What is covered |
|---|---|---|
| `TenderItemTests` | 14 | Constructor validation (null/empty/whitespace id & name), property defaults, setters |
| `TenderAllocationTests` | 5 | Property round-trips, reassignment |
| `TenderSplitResultTests` | 5 | Multi-allocation list, ChangeDue = 0, TotalAllocated |
| `TenderSplitDialogTests` | 32 | Constructor guards, `FormatCurrency`, `AmountToBuffer`, `HandleNumpad` (digit/backspace/dot), `CommitBuffer` (MaxAmount cap, AllowsChange cap), `NavigateTender` (boundary clamp), `SelectTender` (index/buffer sync), `UpdateSummary` (label text, button state), `Confirm` (exact/over/under pay, zero-alloc exclusion, multi-tender split), `Show` guards |

---

## Dependencies

| Dependency | Source |
|---|---|
| `System.Windows.Forms` | .NET Framework 4.7.2 |
| `System.Drawing` | .NET Framework 4.7.2 |
| `System.Core` / `System.Xml.Linq` | .NET Framework 4.7.2 |
| `Microsoft.CSharp` | COM interop support |
| `MSTest.TestFramework` 3.1.1 | NuGet (test project only) |
| `MSTest.TestAdapter` 3.1.1 | NuGet (test project only) |
| `Microsoft.NET.Test.Sdk` 17.8.0 | NuGet (test project only) |

---

## Build & Test

```powershell
# Build the library
msbuild SmartTenderWindow\SmartTenderWindow.csproj /p:Configuration=Debug

# Build and run tests (SDK-style project)
dotnet test SmartTenderWindow.Tests\SmartTenderWindow.Tests.csproj

# Build everything via solution
dotnet build SmartTenderWindow.slnx
```

---

## Usage

```csharp
using SmartTenderWindowTenderSplit.Forms;
using SmartTenderWindowTenderSplit.Models;

var tenders = new List<TenderItem>
{
    new TenderItem("CASH", "Numerário")   { AllowsChange = true,  PreloadedAmount = 50m },
    new TenderItem("CARD", "Cartão")      { AllowsChange = false, MaxAmount = 500m },
    new TenderItem("CHECK", "Cheque")     { AllowsChange = false },
};

TenderSplitResult result = TenderSplitDialog.Show(
    owner: this,
    tenders: tenders,
    documentTotal: 37.50m,
    title: "Pagamento"
);

if (result != null)
{
    foreach (var alloc in result.Allocations)
        Console.WriteLine($"{alloc.Tender.Name}: {alloc.Amount:N2} €");
    Console.WriteLine($"Troco: {result.ChangeDue:N2} €");
}
```

### Validation rules enforced automatically

- At least one tender amount must be greater than zero.
- `TotalAllocated` must be ≥ `documentTotal` to enable confirm.
- For tenders where `AllowsChange = false`, the entered amount is capped at `documentTotal`.
- If a `TenderItem.MaxAmount` is set, the input is capped at that value (takes priority over the AllowsChange cap).

---

## Integration with SmartFramework.Sage50c

1. `SmartApp` opens a POS transaction via `DocumentController` / `TenderTransactionController`.
2. The host calls `TenderSplitDialog.Show()` to let the user allocate payment.
3. The returned `TenderSplitResult.Allocations` are applied to the Sage 50c transaction using `TenderLineItemsUtility`.
4. The transaction is committed through the Sage COM API.

---

## Namespaces

| Namespace | Contents |
|---|---|
| `SmartTenderWindowTenderSplit.Forms` | `TenderSplitDialog` |
| `SmartTenderWindowTenderSplit.Models` | `TenderItem`, `TenderAllocation`, `TenderSplitResult` |
| `SmartTenderWindow.Tests.Helpers` | `StaHelper`, `ReflectionHelper` |
| `SmartTenderWindow.Tests.Models` | Model test classes |
| `SmartTenderWindow.Tests.Forms` | Dialog test classes |
