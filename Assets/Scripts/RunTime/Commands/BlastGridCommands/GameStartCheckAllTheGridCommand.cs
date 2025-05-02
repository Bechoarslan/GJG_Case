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

            // Bütün grid'i tarıyoruz
            foreach (var key in _blastDictionary.Keys)
            {
                // Bu hücreyi daha önce ziyaret etmediysek, işleme alıyoruz
                if (visited.Contains(key)) continue;

                var matched = FindSameColor(key, visited); // Aynı renkli blokları bul
                if (matched.Count > 0) // Eşleşen blok varsa, onları ekliyoruz
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

                visited.Add(current);
                var currentObj = _blastDictionary[current];
                var currentColor = currentObj.color;

                // Debug: Hangi hücreye gidiyoruz ve rengini kontrol ediyoruz
                Debug.Log($"Ziyaret Edilen: {current}, Renk: {currentColor}");

                if (currentColor != startColor) continue;

                matched.Add(current);
        
                // Debug: Eşleşenleri yazdır
                Debug.Log($"Eşleşen: {current}");

                Vector2Int[] directions = { Vector2Int.up, Vector2Int.down, Vector2Int.left, Vector2Int.right };
                foreach (var dir in directions)
                {
                    Vector2 neighbor = current + dir;
                    Debug.Log($"Kontrol edilen yön: {dir}, Komşu hücre: {neighbor}");
                    if (_blastDictionary.ContainsKey(neighbor))
                    {
                        Debug.Log($"Komşu bulundu: {neighbor}");
                        stack.Push(neighbor);
                    }
                    else
                    {
                        Debug.Log($"Komşu bulunamadı: {neighbor}");
                    }
                }

            }

          
            // Debug: Eşleşme yoksa bunu logla
            if (matched.Count == 0)
            {
                Debug.Log($"No match found for start position: {startPos}");
            }

            return matched;
        }
        private bool IsValidNeighbor(Vector2 neighbor)
        {
            return _blastDictionary.ContainsKey(neighbor);
        }
    }
}