using System.Drawing;

namespace SmartTenderWindowTenderSplit.Models
{
    public class TenderDialogColorScheme
    {
        // Header (column header bar)
        public Color HeaderBackColor { get; set; } = Color.FromArgb(76, 175, 80);
        public Color HeaderTextColor { get; set; } = Color.White;

        // Row selection
        public Color SelectedRowColor { get; set; } = Color.FromArgb(200, 230, 201);
        public Color AlternatingRowColor { get; set; } = Color.FromArgb(240, 240, 240);
        public Color DefaultRowColor { get; set; } = Color.White;

        // Status and feedback
        public Color ErrorColor { get; set; } = Color.FromArgb(211, 47, 47);
        public Color DisabledColor { get; set; } = Color.FromArgb(180, 180, 180);
        public Color SuccessColor { get; set; } = Color.FromArgb(76, 175, 80);

        // Text colors
        public Color HeaderForeColor { get; set; } = Color.White;
        public Color ErrorTextColor { get; set; } = Color.FromArgb(211, 47, 47);
        public Color SuccessTextColor { get; set; } = Color.FromArgb(76, 175, 80);

        /// <summary>
        /// Creates a new color scheme with default colors.
        /// </summary>
        public TenderDialogColorScheme()
        {
        }

        /// <summary>
        /// Creates a copy of an existing color scheme.
        /// </summary>
        public TenderDialogColorScheme(TenderDialogColorScheme source)
        {
            if (source != null)
            {
                HeaderBackColor = source.HeaderBackColor;
                HeaderTextColor = source.HeaderTextColor;
                SelectedRowColor = source.SelectedRowColor;
                AlternatingRowColor = source.AlternatingRowColor;
                DefaultRowColor = source.DefaultRowColor;
                ErrorColor = source.ErrorColor;
                DisabledColor = source.DisabledColor;
                SuccessColor = source.SuccessColor;
                HeaderForeColor = source.HeaderForeColor;
                ErrorTextColor = source.ErrorTextColor;
                SuccessTextColor = source.SuccessTextColor;
            }
        }
    }
}
