using System;
using Microsoft.Xna.Framework;
using MLEM.Textures;

namespace MLEM.Ui.Format {
    public class FormattingCode {

        public readonly Type CodeType;
        public readonly Color Color;
        public readonly TextStyle Style;
        public readonly TextureRegion Icon;

        public FormattingCode(Color color) {
            this.Color = color;
            this.CodeType = Type.Color;
        }

        public FormattingCode(TextStyle style) {
            this.Style = style;
            this.CodeType = Type.Style;
        }

        public FormattingCode(TextureRegion icon) {
            this.Icon = icon;
            this.CodeType = Type.Icon;
        }

        public string GetReplacementString() {
            return this.CodeType == Type.Icon ? TextFormatting.OneEmString : string.Empty;
        }

        public enum Type {

            Color,
            Style,
            Icon

        }

    }

    public enum TextStyle {

        Regular,
        Bold,
        Italic

    }
}