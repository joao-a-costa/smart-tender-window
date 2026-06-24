namespace SmartTenderWindowTenderSplit.Models
{
    /// <summary>
    /// A single selectable option for a popup drop-down (bank account, bank, série…).
    /// Supplied by the caller on the <see cref="TenderItem"/>; the library has no
    /// database / ERP access of its own.
    /// </summary>
    public class TenderOption
    {
        public string Id { get; set; }
        public string Display { get; set; }

        public TenderOption() { }

        public TenderOption(string id, string display)
        {
            Id = id;
            Display = display;
        }

        // Used as the visible text inside the ComboBox.
        public override string ToString() => Display;
    }
}
