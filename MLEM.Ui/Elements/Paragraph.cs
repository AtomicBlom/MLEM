using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MLEM.Extensions;
using MLEM.Font;
using MLEM.Textures;
using MLEM.Ui.Format;
using MLEM.Ui.Style;

namespace MLEM.Ui.Elements {
    public class Paragraph : Element {

        private string text;
        private string splitText;
        private Dictionary<int, FormattingCode> codeLocations;
        private IGenericFont regularFont;
        private IGenericFont boldFont;
        private IGenericFont italicFont;

        public NinePatch Background;
        public Color BackgroundColor;
        public Color TextColor = Color.White;
        public float TextScale;
        public string Text {
            get => this.text;
            set {
                if (this.text != value) {
                    this.text = value;
                    this.SetAreaDirty();
                }
            }
        }
        public bool AutoAdjustWidth;
        public TextCallback GetTextCallback;

        public Paragraph(Anchor anchor, float width, TextCallback textCallback, bool centerText = false)
            : this(anchor, width, "", centerText) {
            this.GetTextCallback = textCallback;
            this.Text = textCallback(this);
        }

        public Paragraph(Anchor anchor, float width, string text, bool centerText = false) : base(anchor, new Vector2(width, 0)) {
            this.text = text;
            this.AutoAdjustWidth = centerText;
            this.CanBeSelected = false;
            this.CanBeMoused = false;
        }

        protected override Point CalcActualSize(Rectangle parentArea) {
            var size = base.CalcActualSize(parentArea);

            var sc = this.TextScale * this.Scale;
            this.splitText = this.regularFont.SplitString(this.text.RemoveFormatting(), size.X - this.ScaledPadding.X * 2, sc);
            this.codeLocations = this.text.GetFormattingCodes();

            var textDims = this.regularFont.MeasureString(this.splitText) * sc;
            return new Point(this.AutoAdjustWidth ? textDims.X.Ceil() + this.ScaledPadding.X * 2 : size.X, textDims.Y.Ceil() + this.ScaledPadding.Y * 2);
        }

        public override void Update(GameTime time) {
            base.Update(time);
            if (this.GetTextCallback != null)
                this.Text = this.GetTextCallback(this);
        }

        public override void Draw(GameTime time, SpriteBatch batch, float alpha) {
            if (this.Background != null)
                batch.Draw(this.Background, this.Area, this.BackgroundColor * alpha);

            var pos = this.DisplayArea.Location.ToVector2();
            var sc = this.TextScale * this.Scale;

            // if we don't have any formatting codes, then we don't need to do complex drawing
            if (this.codeLocations.Count <= 0) {
                this.regularFont.DrawString(batch, this.splitText, pos, this.TextColor * alpha, 0, Vector2.Zero, sc, SpriteEffects.None, 0);
            } else {
                // if we have formatting codes, we should do it
                this.regularFont.DrawFormattedString(batch, pos, this.splitText, this.codeLocations, this.TextColor * alpha, sc, this.boldFont, this.italicFont);
            }
            base.Draw(time, batch, alpha);
        }

        protected override void InitStyle(UiStyle style) {
            base.InitStyle(style);
            this.TextScale = style.TextScale;
            this.regularFont = style.Font;
            this.boldFont = style.BoldFont ?? style.Font;
            this.italicFont = style.ItalicFont ?? style.Font;
        }

        public delegate string TextCallback(Paragraph paragraph);

    }

}