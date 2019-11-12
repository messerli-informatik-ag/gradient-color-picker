using System.Drawing;

namespace gradient_color_picker
{
    internal class ColorRange
    {
        public readonly Color _from;

        public readonly Color _to;

        public ColorRange(Color from, Color to)
        {
            _from = from;
            _to = to;
        }
    }
}
