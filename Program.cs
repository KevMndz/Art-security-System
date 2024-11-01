using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ChipSecuritySystem
{
    class Program
    {
        static void Main(string[] args)
        {
            //List<ColorChip> chips = new List<ColorChip>
            //{
            //    //Example
            //    new ColorChip(Color.Blue, Color.Yellow),
            //    new ColorChip(Color.Red, Color.Green),
            //    new ColorChip(Color.Yellow, Color.Red),
            //    new ColorChip(Color.Orange, Color.Purple)
            //};

            List<ColorChip> chips = new List<ColorChip>
            {
                new ColorChip(Color.Red, Color.Purple),
                new ColorChip(Color.Yellow, Color.Orange),
                new ColorChip(Color.Purple, Color.Green),
                new ColorChip(Color.Orange, Color.Red),
                new ColorChip(Color.Blue, Color.Yellow)
            };

            var result = FindCombinationPath(chips, Color.Blue, Color.Green);

            if (result != null && result.Any())
            {
                foreach (var chip in result)
                {
                    Console.WriteLine(chip);
                }
            }
            else
            {
                Console.WriteLine(Constants.ErrorMessage);
            }

            Console.ReadLine();
        }

        static List<ColorChip> FindCombinationPath(List<ColorChip> chips, Color startColor, Color endColor)
        {
            return FindPathRecursive(chips, startColor,new Color(), endColor, new List<ColorChip>());
        }

        static List<ColorChip> FindPathRecursive(List<ColorChip> chips, Color currentColor, Color lastColor, Color endColor, List<ColorChip> currentPath)
        {
            if (lastColor == currentColor && currentColor == endColor && currentPath.Count > 0)
            {
                return new List<ColorChip>(currentPath); 
            }

            List<ColorChip> longestPath = null;

            for (int i = 0; i < chips.Count; i++)
            {
                var chip = chips[i];

                if (chip.StartColor == currentColor || chip.EndColor == currentColor)
                {
                    Color nextColor = chip.StartColor;
                    // Determinamos el próximo color para continuar la secuencia
                    if (chip.StartColor == currentColor) {
                        nextColor = chip.EndColor;
                        lastColor = chip.EndColor;
                    }
                        
                    var newPath = new List<ColorChip>(currentPath) { chip };
                    var remainingChips = new List<ColorChip>(chips);
                    remainingChips.RemoveAt(i);

                    var candidatePath = FindPathRecursive(remainingChips, nextColor,lastColor, endColor, newPath);

                    if (longestPath == null || (candidatePath != null && candidatePath.Count > longestPath.Count))
                    {
                        longestPath = candidatePath;
                    }
                }
            }

            return longestPath;
        }
    }
}