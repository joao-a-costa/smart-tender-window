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
│   │   ├── TenderSplitDialog.cs              # Main dialog – logic & event handling
│   │   ├── TenderSplitDialog.Designer.cs     # Designer-generated UI layout
│   │   ├── TenderSplitDialog.resx
│   │   ├── BankTransferDialog.cs             # Popup for wire transfer details
│   │   ├── BankTransferDialog.Designer.cs
│   │   ├── BankTransferDialog.resx
│   │   ├── CheckDialog.cs                    # Popup for check details
│   │   ├── CheckDialog.Designer.cs
│   │   ├── CheckDialog.resx
│   │   ├── CreditNoteRefundDialog.cs         # Popup for voucher/credit note details
│   │   ├── CreditNoteRefundDialog.Designer.cs
│   │   └── CreditNoteRefundDialog.resx
│   ├── Models/
│   │   ├── TenderTypeEnum.cs                 # Payment method types (cash, card, check, wire, etc.)
│   │   ├── TenderItem.cs                     # Represents one payment method
│   │   ├── TenderAllocation.cs               # Amount + details for a single tender
│   │   ├── TenderSplitResult.cs              # Full result returned on confirmation
│   │   ├── BankTransferDetails.cs            # Wire transfer details (account, reference, date)
│   │   ├── CheckDetails.cs                   # Check details (number, bank, date, amount)
│   │   ├── CreditNoteRefundDetails.cs        # Credit note details (serial, doc#, amounts)
│   │   ├── TenderOption.cs                   # Dropdown option (Id + display text)
│   │   ├── TenderDialogColorScheme.cs        # Customizable color theme
│   │   ├── TenderOptionsNeededEventArgs.cs   # Event args for lazy-loading dropdown options
│   │   └── SmartTenderWindow.csproj          # .NET Framework 4.7.2, old-style csproj
│
├── SmartTenderWindow.Windows/                # WinForms test harness (WinExe)
│   ├── MainForm.cs                           # UI to pick tender count & document total; wires TenderOptionsNeeded event
│   ├── MainForm.Designer.cs
│   ├── Program.cs
│   └── SmartTenderWindow.Windows.csproj
│
├── SmartTenderWindow.Tests/                  # MSTest unit test project
│   ├── Helpers/
│   │   ├── StaHelper.cs                      # Runs actions on STA thread (required for WinForms)
│   │   └── ReflectionHelper.cs               # Invokes private methods/fields by name
│   ├── Models/
│   │   ├── TenderItemTests.cs                # 14 tests
│   │   ├── TenderAllocationTests.cs          # 12 tests (added detail property tests)
│   │   └── TenderSplitResultTests.cs         # 5 tests
│   ├── Forms/
│   │   ├── TenderSplitDialogTests.cs         # 33 tests (added end-to-end test)
│   │   └── TenderDetailDialogsTests.cs       # 7 tests (popup dialogs & details)
│   └── SmartTenderWindow.Tests.csproj        # SDK-style, net472, MSTest 3.1.1
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
TenderSplitDialog(IEnumerable<TenderItem> tenders, decimal documentTotal, 
                  Keys confirmHotkey = Keys.F10, 
                  TenderDialogColorScheme colorScheme = null)
```

- Throws `ArgumentNullException` when `tenders` is null.
- Throws `ArgumentException` when `tenders` is empty.
- Throws `ArgumentOutOfRangeException` when `documentTotal` ≤ 0.
- Preloads `_amounts[]` from each `TenderItem.PreloadedAmount`.
- `confirmHotkey`: keyboard key to trigger confirm (default F10). Can be any `Keys` enum value.
- `colorScheme`: optional `TenderDialogColorScheme` for custom colors. Defaults to built-in green theme if null.
- Calls `BuildTenderGrid()` then `SelectTender(0)`.
- First tender auto-selected on open for convenience.

#### Static convenience method

```csharp
TenderSplitResult Show(IWin32Window owner, IEnumerable<TenderItem> tenders,
                       decimal documentTotal, string title = null,
                       Keys confirmHotkey = Keys.F10,
                       TenderDialogColorScheme colorScheme = null)
```

Shows the dialog modally. Returns a `TenderSplitResult` on confirmation, or `null` on cancel.
- `title`: optional window title (default "Tender Split").
- `confirmHotkey`: keyboard shortcut to confirm (default F10). Can be parameterized to any key.
- `colorScheme`: optional custom color theme. If null, uses default green theme.

#### Key private methods

| Method | Description |
|---|---|
| `BuildTenderGrid()` | Dynamically creates a DataGridView with two columns (Tender Name, Amount). Applies styling: green header, alternating row colors (white/light gray). Sets `TabStop=false` so grid doesn't steal focus. Wires up cell editing, selection, and keyboard handlers. |
| `SelectTender(int index)` | Highlights the selected row in grid, syncs `_inputBuffer` to the current amount. Shows Details button if tender type requires popup. No-op when index equals `_selectedIndex`. |
| `NavigateTender(int delta)` | Moves selection by `delta`, clamped to `[0, count-1]`. Scrolls grid to keep selected row visible. |
| `HandleNumpad(string key)` | Cash-register input: digits append right, `"⌫"` removes last digit (floors to `"0"`), `"."` and `","` are no-ops (2 decimal places are implied). Calls `CommitBuffer()`. Auto-enters cell edit mode on first numeric key press. |
| `CommitBuffer()` | Parses `_inputBuffer` as `raw / 100m`. Enforces `MaxAmount` cap, then enforces `documentTotal` cap for `AllowsChange = false` tenders. |
| `UpdateSummary()` | Recalculates delivered / missing. Shows **"Em falta:"** (red) when underpaid, **"Troco:"** (green) when overpaid or exact. Enables/disables `btnConfirm`. |
| `Confirm()` | Guards against underpayment and missing details. Builds `TenderSplitResult` with allocations (only Amount > 0) carrying captured details, sets `DialogResult = OK`. |
| `FinalizeInputAndOpenDetailsIfNeeded()` | Commits current amount and auto-opens popup dialog for tenders requiring details (wire, check, voucher). |
| `FirstMissingDetailIndex()` | Returns index of first tender with Amount > 0 but missing required details, or -1 if all complete. |
| `RequiresPopup(TenderTypeEnum?)` | Returns true for `tndBankWireTransfer`, `tndCheck`, `tndVoucher`; false otherwise. |
| `EnsureOptionsLoaded(int, TenderItem)` | Fires `TenderOptionsNeeded` event once per tender (guarded by `_optionsLoaded[]` cache). Writes returned lists back onto the `TenderItem`. No-op on subsequent calls for the same index. |
| `FormatCurrency(decimal)` | Returns `value.ToString("N2") + " €"`. |
| `AmountToBuffer(decimal)` | Returns `((long)(value * 100)).ToString()`, or `"0"` for zero. |

#### UI layout

```
┌────────────────────────────────────────────────────────────┐
│  [Green header]  Title               Total: 37,50 €        │  52px, DockStyle.Top
├──────────────────────────────────────────┬─────────────────┤
│  [DataGridView: tender list]             │  [7] [8] [9]    │
│  ┌─ Modalidade ──────────┬─ Valor (€) ┐  │  [4] [5] [6]    │
│  │ Numerário             │ 0,00 €     │  │  [1] [2] [3]    │
│  │ Cartão                │ 0,00 €     │  │  [⌫] [0] [.]    │
│  │ …                     │ …          │  │  [    OK    ]   │
│  └───────────────────────┴────────────┘  │                 │
│  [▲] [▼]                                 │  370px numpad,  │
│  Valor entregue:  0,00 €                 │  DockStyle.Right│
│  Em falta:       37,50 €                 │                 │
│  Total:          37,50 €                 │                 │
│  [Confirmar]  [Cancelar]  [Detalhes]     │                 │
└──────────────────────────────────────────┴─────────────────┘
```

**Features:**
- `FormBorderStyle.Sizable`, `WindowState.Maximized`, `KeyPreview = true`
- DataGridView with green header, alternating row colors (white/light gray)
- Delete key deletes selected text in cells (via `ProcessCmdKey` override)
- Grid TabStop=false prevents focus stealing; OK button stays focused
- Click rows to select; arrow buttons navigate and scroll to keep row visible
- Keyboard: 0–9 → numpad; Backspace → delete digit; Delete (in cell) → delete selected text; ↑/↓ → navigate; Enter → confirm; Escape → cancel
- When OK clicked with no amount entered, fills selected tender with full document total

---

### `Models/TenderTypeEnum.cs`

```csharp
public enum TenderTypeEnum
{
    tndCash = 0,
    tndCreditDebitCard = 1,
    tndMBWay = 2,
    tndBankWireTransfer = 4,
    tndCheck = 5,
    tndRefMB = 6,
    tndVoucher = 7,
    tndCreditNote = 8,
    tndDebitNote = 9,
    tndCompensation = 10,
    tndWallet = 11,
    tndAdvance = 13,
    tndCustomerAdvance = 14,
    tndStandardizedVoucher = 15
}
```

Maps payment method types to Sage 50c tender codes.

---

### `Models/TenderItem.cs`

```csharp
public class TenderItem
{
    public TenderTypeEnum? TenderType { get; set; }
    public string Name { get; set; }
    public decimal PreloadedAmount { get; set; }   // Default starting value (default 0)
    public decimal? MaxAmount { get; set; }         // Optional hard cap (default null)
    public bool AllowsChange { get; set; }          // true = overpayment → change; false = capped at total
    
    // Populated lazily via TenderOptionsNeeded event (do NOT pre-fill; dialog sets these from event args)
    public List<TenderOption> BeneficiaryAccounts { get; set; }  // For wire transfers
    public List<TenderOption> PartyAccounts { get; set; }        // For wire transfers
    public List<TenderOption> Banks { get; set; }                // For checks
    public List<TenderOption> Series { get; set; }               // For vouchers/credit notes
}
```

Two-arg constructor throws `ArgumentException` when `Name` is null/empty/whitespace. Default (no-arg) constructor also available.

---

### `Models/TenderAllocation.cs`

```csharp
public class TenderAllocation
{
    public TenderItem Tender { get; set; }
    public decimal Amount { get; set; }
    
    // Details for special tender types (only one populated per allocation)
    public BankTransferDetails BankTransfer { get; set; }
    public CheckDetails Check { get; set; }
    public CreditNoteRefundDetails CreditNoteRefund { get; set; }
}
```

---

### `Models/BankTransferDetails.cs`

```csharp
public class BankTransferDetails
{
    public string BankAccountId { get; set; }           // Beneficiary account ID
    public string PartyBankAccountId { get; set; }      // Party account ID
    public string ContractReferenceNumber { get; set; } // Contract/reference number
    public DateTime AccountingDate { get; set; }        // Value date
}
```

---

### `Models/CheckDetails.cs`

```csharp
public class CheckDetails
{
    public string CheckSequenceNumber { get; set; }  // Mandatory check number
    public string BankId { get; set; }               // Bank ID
    public DateTime CheckDeferredDate { get; set; }  // Deferred date (if any)
    public decimal CheckAmount { get; set; }         // Check amount
}
```

---

### `Models/CreditNoteRefundDetails.cs`

```csharp
public class CreditNoteRefundDetails
{
    public string TransSerial { get; set; }         // Document series ID
    public string TransDocument { get; set; }       // Document type ("Nota de Crédito-Reembolso")
    public long TransDocNumber { get; set; }        // Document number
    public decimal TotalAmount { get; set; }        // Total available amount
    public decimal SpentValueAmount { get; set; }   // Amount to deduct/spend
}
```

---

### `Models/TenderOption.cs`

```csharp
public class TenderOption
{
    public string Id { get; set; }          // Option ID (account, bank, series code)
    public string DisplayText { get; set; } // Display name for combo boxes
}
```

---

### `Models/TenderDialogColorScheme.cs`

Customizable color theme for the tender dialog. All colors are configurable; defaults to green POS theme.

```csharp
public class TenderDialogColorScheme
{
    public Color HeaderBackColor { get; set; }      // Header bar background (default: green #4CAF50)
    public Color HeaderTextColor { get; set; }      // Header text (default: white)
    public Color SelectedRowColor { get; set; }     // Selected tender row (default: light green #C8E6C9)
    public Color AlternatingRowColor { get; set; }  // Even rows (default: light gray #F0F0F0)
    public Color DefaultRowColor { get; set; }      // Odd rows (default: white)
    public Color ErrorColor { get; set; }           // Error accent (default: red #D32F2F)
    public Color DisabledColor { get; set; }        // Disabled button (default: gray #B4B4B4)
    public Color SuccessColor { get; set; }         // Success accent (default: green #4CAF50)
    public Color HeaderForeColor { get; set; }      // Header foreground (default: white)
    public Color ErrorTextColor { get; set; }       // "Em falta" error text (default: red #D32F2F)
    public Color SuccessTextColor { get; set; }     // "Troco" success text (default: green #4CAF50)
}
```

Pass to `TenderSplitDialog.Show()` via the `colorScheme` parameter to customize appearance.

---

### `Models/TenderOptionsNeededEventArgs.cs`

Event args for the `TenderOptionsNeeded` event. Fired once per tender that requires a popup, just before that popup opens. The caller sets whichever list properties are relevant for the `TenderType`; the dialog writes them back onto the `TenderItem` and caches that the load has happened.

```csharp
public class TenderOptionsNeededEventArgs : EventArgs
{
    public TenderItem Tender { get; }          // The tender that needs options
    public TenderTypeEnum? TenderType { get; } // Shortcut for switching
    public int TenderIndex { get; }            // Index in the tenders list

    // Set whichever are relevant for the TenderType:
    public List<TenderOption> BeneficiaryAccounts { get; set; }  // tndBankWireTransfer
    public List<TenderOption> PartyAccounts { get; set; }        // tndBankWireTransfer
    public List<TenderOption> Banks { get; set; }                // tndCheck
    public List<TenderOption> Series { get; set; }               // tndVoucher
}
```

**Pattern:** wire the event before showing the dialog; switch on `TenderType` to set only what's needed.

```csharp
dialog.TenderOptionsNeeded += (s, e) => {
    switch (e.TenderType) {
        case TenderTypeEnum.tndBankWireTransfer:
            e.BeneficiaryAccounts = LoadBeneficiaryAccounts();
            e.PartyAccounts       = LoadPartyAccounts();
            break;
        case TenderTypeEnum.tndCheck:
            e.Banks = LoadBanks();
            break;
        case TenderTypeEnum.tndVoucher:
            e.Series = LoadSeries();
            break;
    }
};
```

---

### `Models/TenderSplitResult.cs`

```csharp
public class TenderSplitResult
{
    public List<TenderAllocation> Allocations { get; set; }  // Only entries with Amount > 0; includes captured details
    public decimal TotalAllocated { get; set; }
    public decimal ChangeDue { get; set; }                   // 0 when no overpayment
}
```

---

### `Forms/BankTransferDialog.cs`

Modal popup for capturing wire transfer details. Triggered when user selects a `tndBankWireTransfer` tender.

**Layout:**
- Header: "Transferência bancária"
- Beneficiary account dropdown (from `TenderItem.BeneficiaryAccounts`)
- Party account dropdown (from `TenderItem.PartyAccounts`)
- Contract reference textbox
- Value date picker
- OK / Cancel buttons

**Result:** Returns `BankTransferDetails` on OK, null on cancel.

---

### `Forms/CheckDialog.cs`

Modal popup for capturing check details. Triggered when user selects a `tndCheck` tender.

**Layout:**
- Header: "Cheque"
- Check number textbox (mandatory; yellow background when empty)
- Bank dropdown (from `TenderItem.Banks`)
- Deferred date picker
- Amount field (numeric)
- OK / Cancel buttons

**Validation:** Check number is required; `OnOk()` returns null if empty.

**Result:** Returns `CheckDetails` on OK, null on cancel.

---

### `Forms/CreditNoteRefundDialog.cs`

Modal popup for capturing credit note / voucher details. Triggered when user selects a `tndVoucher` tender.

**Layout:**
- Header: "Dados do documento"
- Series dropdown (from `TenderItem.Series`)
- Document type textbox (read-only: "Nota de Crédito-Reembolso")
- Document number numeric field
- Available amount numeric field
- Amount to deduct numeric field (pre-filled with tender allocation amount)
- OK / Cancel buttons

**Result:** Returns `CreditNoteRefundDetails` on OK, null on cancel.

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
| `TenderItemTests` | 14 | Constructor validation (null/empty/whitespace name), TenderType enum, property defaults, setters |
| `TenderAllocationTests` | 12 | Property round-trips (Amount, Tender), BankTransfer/Check/CreditNoteRefund detail properties |
| `TenderSplitResultTests` | 5 | Multi-allocation list, ChangeDue = 0, TotalAllocated |
| `TenderSplitDialogTests` | 33 | Constructor guards, `FormatCurrency`, `AmountToBuffer`, `HandleNumpad` (digit/backspace/dot), `CommitBuffer` (MaxAmount cap, AllowsChange cap), `NavigateTender` (boundary clamp), `SelectTender` (index/buffer sync), `UpdateSummary` (label text, button state), `Confirm` (exact/over/under pay, zero-alloc exclusion, multi-tender split, auto-fill on OK), `Show` guards, end-to-end multi-tender scenario |
| `TenderDetailDialogsTests` | 7 | `RequiresPopup` logic, `FirstMissingDetailIndex`, details capture and return in allocations, popup dialogs (BankTransferDialog, CheckDialog, CreditNoteRefundDialog), mandatory field validation |

**Total: 83 tests, all passing.**

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
    new TenderItem(TenderTypeEnum.tndCash, "Numerário") 
    { 
        AllowsChange = true, 
        PreloadedAmount = 50m 
    },
    new TenderItem(TenderTypeEnum.tndCreditDebitCard, "Cartão") 
    { 
        AllowsChange = false, 
        MaxAmount = 500m 
    },
    new TenderItem(TenderTypeEnum.tndCheck, "Cheque") 
    { 
        AllowsChange = false
        // Do NOT pre-fill Banks — supply them via TenderOptionsNeeded event
    },
};

// Recommended: instantiate directly to wire TenderOptionsNeeded before showing
using (var dialog = new TenderSplitDialog(tenders, 37.50m))
{
    dialog.Text = "Pagamento";
    dialog.TenderOptionsNeeded += (s, e) =>
    {
        switch (e.TenderType)
        {
            case TenderTypeEnum.tndBankWireTransfer:
                e.BeneficiaryAccounts = LoadBeneficiaryAccounts();
                e.PartyAccounts       = LoadPartyAccounts();
                break;
            case TenderTypeEnum.tndCheck:
                e.Banks = LoadBanks();
                break;
            case TenderTypeEnum.tndVoucher:
                e.Series = LoadSeries();
                break;
        }
    };
    TenderSplitResult result = dialog.ShowDialog(owner) == DialogResult.OK ? dialog.Result : null;
}

// With custom confirm hotkey and color scheme (using static Show — no event support)
var customColors = new TenderDialogColorScheme
{
    HeaderBackColor = Color.DarkBlue,
    SelectedRowColor = Color.LightBlue,
    ErrorTextColor = Color.Red,
    SuccessTextColor = Color.Green
};

TenderSplitResult result = TenderSplitDialog.Show(
    owner: this,
    tenders: tenders,
    documentTotal: 37.50m,
    title: "Pagamento",
    confirmHotkey: Keys.F10,
    colorScheme: customColors
);

if (result != null)
{
    foreach (var alloc in result.Allocations)
        Console.WriteLine($"{alloc.Tender.Name}: {alloc.Amount:N2} €");
    Console.WriteLine($"Troco: {result.ChangeDue:N2} €");
}
```

### Keyboard & UI Features

- **Grid Edit Mode**: Select a row and start typing numbers → automatically enters edit mode on the Amount cell. Period (`.`) and comma (`,`) are no-ops; both are treated as decimal separators and ignored (system enforces 2 decimal places internally).
- **Confirm Hotkey**: Default is **F10**, but any `Keys` enum value can be set via `confirmHotkey` parameter.
- **Cell Editing**: When in edit mode, TextBox handles input normally. `CellValueChanged` parses the value (accepts both `.` and `,` as decimal separators).
- **Arrow Keys**: ↑/↓ navigate between tenders; selection scrolls to keep current row visible.
- **Backspace/Delete**: Remove last digit in buffer (outside edit mode); behave normally in edit mode.
- **Escape**: Cancel dialog.
- **Enter**: Confirm if all validations pass.

### Validation rules enforced automatically

- At least one tender amount must be greater than zero.
- `TotalAllocated` must be ≥ `documentTotal` to enable confirm.
- For tenders where `AllowsChange = false`, the entered amount is capped at `documentTotal`.
- If a `TenderItem.MaxAmount` is set, the input is capped at that value (takes priority over the AllowsChange cap).
- For tenders of type `tndBankWireTransfer`, `tndCheck`, or `tndVoucher`: the corresponding detail popup must be completed before confirmation is allowed.
- When OK is clicked with no amount entered, the selected tender is auto-filled with the full document total.
- Delete key in grid cells deletes selected text (via `ProcessCmdKey` override).
- Grid TabStop=false prevents focus stealing; OK button remains focused for keyboard input.

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
| `SmartTenderWindowTenderSplit.Forms` | `TenderSplitDialog`, `BankTransferDialog`, `CheckDialog`, `CreditNoteRefundDialog` |
| `SmartTenderWindowTenderSplit.Models` | `TenderTypeEnum`, `TenderItem`, `TenderAllocation`, `TenderSplitResult`, `BankTransferDetails`, `CheckDetails`, `CreditNoteRefundDetails`, `TenderOption`, `TenderDialogColorScheme`, `TenderOptionsNeededEventArgs` |
| `SmartTenderWindow.Tests.Helpers` | `StaHelper`, `ReflectionHelper` |
| `SmartTenderWindow.Tests.Models` | `TenderItemTests`, `TenderAllocationTests`, `TenderSplitResultTests` |
| `SmartTenderWindow.Tests.Forms` | `TenderSplitDialogTests`, `TenderDetailDialogsTests` |
