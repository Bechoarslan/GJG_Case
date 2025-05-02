using System.Collections.Generic;
using System.Linq;
using RunTime.Data.UnityObject;
using RunTime.Enums;
using RunTime.Keys;
using Sirenix.OdinInspector;
using UnityEngine;

namespace RunTime.Commands.BlastGridCommands
{
    public class FindSameColorCommand
    {
        private Dictionary<Vector2, BlastKeys> _blastDictionary;
        

        public FindSameColorCommand(Dictionary<Vector2, BlastKeys> blastDictionary
           )
        {
            
            _blastDictionary = blastDictionary;
            
        }

        public List<Vector2> Execute(Vector2 newPos)
        {
            List<Vector2> matched = new();
            Stack<Vector2> stack = new();
            HashSet<Vector2> visited = new();

            var startPos = newPos;
            if (!_blastDictionary.ContainsKey(newPos)) return matched;
            var startObj = _blastDictionary[startPos];
            
            var startColor = startObj.color;

            
            
            stack.Push(startPos);

            while (stack.Count > 0)
            {
                var current = stack.Pop();
                if (visited.Contains(current)) continue;

                visited.Add(current);
                var currentObj = _blastDictionary[current];
                var currentColor = currentObj.color;

                if (currentColor != startColor) continue;

                matched.Add(current);

                // Komşulara bak
                Vector2Int[] directions = {
                    Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right
                };

                foreach (var dir in directions)
                {
                    Vector2 neighbor = current + dir;
                    if (_blastDictionary.ContainsKey(neighbor))
                        stack.Push(neighbor);
                }
            }

           
            
            return matched;
            
            
        }

        
        
    }
}

    



       