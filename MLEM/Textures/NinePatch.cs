using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MLEM.Extensions;

namespace MLEM.Textures {
    public class NinePatch {

        public readonly TextureRegion Region;
        public readonly int PaddingLeft;
        public readonly int PaddingRight;
        public readonly int PaddingTop;
        public readonly int PaddingBottom;

        public readonly Rectangle[] SourceRectangles;

        public NinePatch(TextureRegion texture, int paddingLeft, int paddingRight, int paddingTop, int paddingBottom) {
            this.Region = texture;
            this.PaddingLeft = paddingLeft;
            this.PaddingRight = paddingRight;
            this.PaddingTop = paddingTop;
            this.PaddingBottom = paddingBottom;
            this.SourceRectangles = this.CreateRectangles(this.Region.Area).ToArray();
        }

        public NinePatch(Texture2D texture, int paddingLeft, int paddingRight, int paddingTop, int paddingBottom) :
            this(new TextureRegion(texture), paddingLeft, paddingRight, paddingTop, paddingBottom) {
        }

        public NinePatch(Texture2D texture, int padding) : this(new TextureRegion(texture), padding) {
        }

        public NinePatch(TextureRegion texture, int padding) : this(texture, padding, padding, padding, padding) {
        }

        public IEnumerable<Rectangle> CreateRectangles(Rectangle area, float patchScale = 1) {
            var pl = (int) (this.PaddingLeft * patchScale);
            var pr = (int) (this.PaddingRight * patchScale);
            var pt = (int) (this.PaddingTop * patchScale);
            var pb = (int) (this.PaddingBottom * patchScale);

            var centerW = area.Width - pl - pr;
            var centerH = area.Height - pt - pb;
            var leftX = area.X + pl;
            var rightX = area.X + area.Width - pr;
            var topY = area.Y + pt;
            var bottomY = area.Y + area.Height - pb;

            yield return new Rectangle(area.X, area.Y, pl, pt);
            yield return new Rectangle(leftX, area.Y, centerW, pt);
            yield return new Rectangle(rightX, area.Y, pr, pt);
            yield return new Rectangle(area.X, topY, pl, centerH);
            yield return new Rectangle(leftX, topY, centerW, centerH);
            yield return new Rectangle(rightX, topY, pr, centerH);
            yield return new Rectangle(area.X, bottomY, pl, pb);
            yield return new Rectangle(leftX, bottomY, centerW, pb);
            yield return new Rectangle(rightX, bottomY, pr, pb);
        }

    }

    public static class NinePatchExtensions {

        public static void Draw(this SpriteBatch batch, NinePatch texture, Rectangle destinationRectangle, Color color, float rotation, Vector2 origin, SpriteEffects effects, float layerDepth, float patchScale = 1) {
            var dest = texture.CreateRectangles(destinationRectangle, patchScale);
            var count = 0;
            foreach (var rect in dest) {
                if (!rect.IsEmpty)
                    batch.Draw(texture.Region.Texture, rect, texture.SourceRectangles[count], color, rotation, origin, effects, layerDepth);
                count++;
            }
        }

        public static void Draw(this SpriteBatch batch, NinePatch texture, Rectangle destinationRectangle, Color color, float patchScale = 1) {
            batch.Draw(texture, destinationRectangle, color, 0, Vector2.Zero, SpriteEffects.None, 0, patchScale);
        }

    }
}