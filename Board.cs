using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace battleship
{
    class Board
    {
        public List<List<Field>> fields = new List<List<Field>>();

        public Board() { }
        public void CreateBoard()
        {
            for (int i = 0; i < 10; i++)
            {
                fields.Add(new List<Field>());
                for (int j = 0; j < 10; j++)
                {
                    Field field = new Field();
                    field.Empty = true;
                    fields[i].Add(field);
                }
            }
        }
    }
}
