using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace BoxField
{


    class Box
    {
        public Color color;
        public int size, x, y;

        public Box(int _x, int _y, int _size, Color _color)
        {
            x = _x;
            y = _y;
            size = _size;
            color = _color;
        }

        public void Move(int speed)
        {
            y += speed;
            x += speed / 4;
        }

        public void Move(int speed, Boolean direction)
        {
            //true = right;
            if (direction)
            {
                x += speed;
            }
            else
            {
                x -= speed;
            }


        }
        public void Move2(int speed, Boolean direction)
        {
            //true = right;
            if (direction)
            {
                y += speed;
            }
            else
            {
                y -= speed;
            }
        }
    }
}
