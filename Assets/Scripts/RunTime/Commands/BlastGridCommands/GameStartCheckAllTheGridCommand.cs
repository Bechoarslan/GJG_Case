using System.Collections.Generic;
using RunTime.Keys;
using UnityEngine;

namespace RunTime.Commands.BlastGridCommands
{
    public class GameStartCheckAllTheGridCommand
    {
        private Dictionary<Vector2, BlastKeys> _blastDictionary;
        public GameStartCheckAllTheGridCommand(Dictionary<Vector2, BlastKeys> blastDictionary)
        {
            
            _blastDictionary = blastDictionary;
        }

        public List<List<Vector2>> Execute()
        {
            List<List<Vector2>> allMatched = new();
            HashSet<Vector2> visited = new();

            
            foreach (var key in _blastDictionary.Keys)
            {
               
                if (visited.Contains(key)) continue;

                var matched = FindSameColor(key, visited); 
                if (matched.Count >= 5) 
                {
                    allMatched.Add(matched);
                }
            }

            return allMatched;
        }


        private List<Vector2> FindSameColor(Vector2 startPos, HashSet<Vector2> visited)
        {
            List<Vector2> matched = new();
            Stack<Vector2> stack = new();

            var startObj = _blastDictionary[startPos];
            var startColor = startObj.color;

            stack.Push(startPos);

            while (stack.Count > 0)
            {
                var current = stack.Pop();
                if (visited.Contains(current)) continue;

                
                var currentObj = _blastDictionary[current];
                var currentColor = currentObj.color;

             
              

                if (currentColor != startColor) continue;
                visited.Add(current);
                matched.Add(current);
        
              
           

                Vector2Int[] directions = { Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right };
                foreach (var dir in directions)
                {
                    Vector2 neighbor = current + dir;
                 
                    if (_blastDictionary.ContainsKey(neighbor))
                    {
                       
                        stack.Push(neighbor);
                    }
                    
                }

            }

          
            // Debug: Eşleşme yoksa bunu logla
            if (matched.Count == 0)
            {
                
            }

            return matched;
        }
     
    }
}