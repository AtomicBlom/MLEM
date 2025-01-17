using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MLEM.Data;
using MLEM.Textures;
using NUnit.Framework;

namespace Tests {
    public class TestDataTextureAtlas {

        [Test]
        public void Test() {
            using var game = TestGame.Create();
            using var texture = new Texture2D(game.GraphicsDevice, 1, 1);
            var atlas = DataTextureAtlas.LoadAtlasData(new TextureRegion(texture), game.RawContent, "Texture.atlas");
            Assert.AreEqual(atlas.Regions.Count(), 5);

            var table = atlas["LongTableUp"];
            Assert.AreEqual(table.Area, new Rectangle(0, 32, 64, 48));
            Assert.AreEqual(table.PivotPixels, new Vector2(16, 48 - 32));
        }

    }
}