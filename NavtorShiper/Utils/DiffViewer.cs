using DiffPlex.DiffBuilder.Model;
using DiffPlex.DiffBuilder;
using DiffPlex;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Pipelines;

namespace NavtorShiper.Utils
{
    public class DiffViewr
    {
        public static void ShowDiff(string oldText, string newText)
        {
            var differ = new Differ();
            var builder = new SideBySideDiffBuilder(differ);
            var diff = builder.BuildDiffModel(oldText, newText);

            int lineCount = Math.Max(diff.OldText.Lines.Count, diff.NewText.Lines.Count);

            for (int i = 0; i < lineCount; i++)
            {
                var oldLine = i < diff.OldText.Lines.Count ? diff.OldText.Lines[i] : null;
                var newLine = i < diff.NewText.Lines.Count ? diff.NewText.Lines[i] : null;


                if (oldLine?.Type != ChangeType.Deleted 
                    && newLine?.Type != ChangeType.Inserted
                    && oldLine?.SubPieces.Count == 0
                    && newLine?.SubPieces.Count == 0
                    )
                {
                    Console.WriteLine(oldLine?.Text);
                }
                else
                {
                    WriteDiffLine(oldLine, "left");
                    WriteDiffLine(newLine, "right");
                }
            }
        }

        private static void WriteDiffLine(DiffPiece? line, string side)
        {
            if (line is null || line.Type == ChangeType.Imaginary)
            {
                return;
            }
            if (line.SubPieces.Count == 0)
            {
                WriteDiffByWholeLine(line);
            }
            else
            {
                WriteDiffByWords(line);
            }
        }

        private static void WriteDiffByWords(DiffPiece line)
        {
            foreach (var piece in line?.SubPieces)
            {
                SetColors(piece.Type);
                Console.Write(piece.Text);
                Console.ResetColor();
            }

            Console.WriteLine();
        }

        private static void WriteDiffByWholeLine(DiffPiece line)
        {
            SetColors(line.Type);
            Console.Write(line.Text);
            Console.ResetColor();
            Console.WriteLine();
        }

        static void SetColors(ChangeType type)
        {
            switch (type)
            {
                case ChangeType.Inserted:
                    Console.BackgroundColor = ConsoleColor.DarkGreen;
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case ChangeType.Deleted:
                    Console.BackgroundColor = ConsoleColor.DarkRed;
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case ChangeType.Modified:
                    Console.BackgroundColor = ConsoleColor.DarkYellow;
                    Console.ForegroundColor = ConsoleColor.Black;
                    break;
                default:
                    Console.ResetColor();
                    break;
            }
        }
    }
}
    