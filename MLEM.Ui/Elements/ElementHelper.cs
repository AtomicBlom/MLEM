using Microsoft.Xna.Framework;
using MLEM.Input;

namespace MLEM.Ui.Elements {
    public static class ElementHelper {

        public static Group NumberField(Anchor anchor, Vector2 size, int defaultValue = 0, int stepPerClick = 1, TextField.Rule rule = null, TextField.TextChanged onTextChange = null) {
            var group = new Group(anchor, size, false);

            var field = new TextField(Anchor.TopLeft, Vector2.One, rule ?? TextField.OnlyNumbers);
            field.OnTextChange = onTextChange;
            field.AppendText(defaultValue.ToString());
            group.AddChild(field);
            group.OnAreaUpdated += e => field.Size = new Vector2((e.Area.Width - e.Area.Height / 2) / e.Scale, 1);

            var upButton = new Button(Anchor.TopRight, Vector2.One, "+") {
                OnClicked = (element, button) => {
                    if (button == MouseButton.Left) {
                        var text = field.Text.ToString();
                        field.SetText(int.Parse(text) + stepPerClick);
                    }
                }
            };
            group.AddChild(upButton);
            group.OnAreaUpdated += e => upButton.Size = new Vector2(e.Area.Height / 2 / e.Scale);

            var downButton = new Button(Anchor.BottomRight, Vector2.One, "-") {
                OnClicked = (element, button) => {
                    if (button == MouseButton.Left) {
                        var text = field.Text.ToString();
                        field.SetText(int.Parse(text) - stepPerClick);
                    }
                }
            };
            group.AddChild(downButton);
            group.OnAreaUpdated += e => downButton.Size = new Vector2(e.Area.Height / 2 / e.Scale);

            return group;
        }

    }
}