# SmartTenderWindow

![Platform](https://img.shields.io/badge/platform-Windows-blue) ![Framework](https://img.shields.io/badge/.NET-WinForms-purple) ![Language](https://img.shields.io/badge/language-C%23-green) ![Target](https://img.shields.io/badge/.NET%20Framework-4.7.2-orange)

A **C# .NET Windows Forms class library** that provides a POS-style modal payment-tender allocation dialog. It lets users split a document total across multiple payment methods (cash, card, check, etc.), enforcing per-tender caps and computing change due. Designed to integrate with the **SmartFramework.Sage50c** ecosystem.

---

## 🎯 Purpose

SmartTenderWindow gives POS applications a ready-made, touch-friendly payment screen. The operator selects a tender (e.g. Cash, Card, Cheque), keys in the amount on the built-in numpad, and the dialog enforces all business rules in real time — capping non-change tenders at the document total, respecting per-tender maximum amounts, and enabling the Confirm button only when the total is covered. On confirmation it returns a typed result with the full allocation list and change due.

---

## ✨ Features

- 💶 **Multi-tender split** — allocate a document total across any number of payment methods in one dialog
- 🔢 **Cash-register numpad** — POS-style digit input with implied 2 decimal places, matching real terminal behaviour
- ⌨️ **Full keyboard support** — 0–9, Backspace/Delete, ↑/↓ arrows, Enter and Escape all work without touching the mouse
- 🚦 **Live validation** — "Em falta" (red) while underpaid, "Troco" (green) when exact or overpaid; Confirm button mirrors the state
- 🔒 **Per-tender rules** — `MaxAmount` hard cap and `AllowsChange` flag enforced automatically
- 📜 **Scrollable tender list** — supports up to 100 payment methods with ▲/▼ navigation buttons
- 🖥️ **Full-screen POS layout** — maximized window with green header, scrollable list panel, and side numpad
- 🧪 **56 MSTest unit tests** — covers all models, constructor guards, numpad logic, buffer math, and result construction

---

## 🏗️ Architecture

| Project | Role |
|---|---|
| `SmartTenderWindow` | Core class library — dialog form and models (`.dll`) |
| `SmartTenderWindow.Windows` | WinForms test harness — pick tender count & amount, open the dialog |
| `SmartTenderWindow.Tests` | MSTest 3.1.1 unit test project (net472) |

For a full breakdown of every class and private method, see [`CLAUDE.md`](./CLAUDE.md).

---

## 🛠️ Tech Stack

| Technology | Purpose |
|---|---|
| **C# / .NET WinForms** | Core language and UI framework |
| **.NET Framework 4.7.2** | Target runtime |
| **MSTest 3.1.1** | Unit testing framework (test project only) |
| **Microsoft.NET.Test.Sdk 17.8.0** | Test runner host (test project only) |

No third-party NuGet packages are required for the library itself.

---

## ⚡ Quick Start

### Prerequisites

- **Windows** with .NET Framework 4.7.2 or later
- **Visual Studio 2019+** with the Windows Forms workload

### Build

1. Clone the repository:
   ```
   git clone <repo-url>
   cd smart-tender-window
   ```

2. Open the solution in Visual Studio:
   ```
   SmartTenderWindow.slnx
   ```

3. Build via MSBuild:
   ```powershell
   msbuild SmartTenderWindow\SmartTenderWindow.csproj /p:Configuration=Release /p:Platform=AnyCPU
   ```

4. The output DLL is at `SmartTenderWindow\bin\Release\smart-tender-window.dll`.

### Run tests

```powershell
dotnet test SmartTenderWindow.Tests\SmartTenderWindow.Tests.csproj
```

---

## 📋 Usage

Add a reference to `smart-tender-window.dll` (or the `.csproj`) and call the static `Show` method:

```csharp
using SmartTenderWindowTenderSplit.Forms;
using SmartTenderWindowTenderSplit.Models;

var tenders = new List<TenderItem>
{
    new TenderItem("CASH",  "Numerário") { AllowsChange = true,  PreloadedAmount = 50m },
    new TenderItem("CARD",  "Cartão")    { AllowsChange = false, MaxAmount = 500m },
    new TenderItem("CHECK", "Cheque")    { AllowsChange = false },
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

| Rule | Detail |
|---|---|
| Minimum entry | At least one tender amount must be greater than zero |
| Total coverage | Confirm is disabled until `TotalAllocated ≥ documentTotal` |
| Non-change cap | For `AllowsChange = false`, input is capped at `documentTotal` |
| MaxAmount cap | If `TenderItem.MaxAmount` is set, input is capped at that value (takes priority) |

---

## 🔄 Integration with SmartFramework.Sage50c

Typical flow in a Sage 50c POS host:

1. `SmartApp` opens a POS transaction via `DocumentController` / `TenderTransactionController`.
2. The host calls `TenderSplitDialog.Show()` to let the operator allocate payment.
3. The returned `TenderSplitResult.Allocations` are applied to the Sage 50c transaction using `TenderLineItemsUtility`.
4. The transaction is committed through the Sage COM API.

---

## 📖 Documentation Cross-Reference

For deep architecture details including all class members, private method tables, UI layout diagrams, and test coverage, see [`CLAUDE.md`](./CLAUDE.md).

---

## 🏢 License & Support

Developed and maintained by **SmartDigit**.  
For support, contact: [support@smartdigit.pt](mailto:support@smartdigit.pt)
