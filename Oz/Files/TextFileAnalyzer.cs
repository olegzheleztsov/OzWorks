using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Oz.Algorithms.Numerics;

namespace Oz.Files
{
    public sealed class TextFileAnalyzer : IDisposable
    {
        private readonly StreamReader _streamReader;

        public TextFileAnalyzer(string filePath)
        {
            _streamReader = new StreamReader(filePath);
        }

        public void Dispose()
        {
            _streamReader?.Dispose();
        }

        public async Task<Dictionary<char, CharacterInfo>> GetCharacterInfosAsync()
        {
            var characterInfos = new Dictionary<char, CharacterInfo>();
            string line;
            while ((line = await _streamReader.ReadLineAsync()) != null)
            {
                foreach (var c in line)
                {
                    if (characterInfos.ContainsKey(c))
                    {
                        characterInfos[c].IncrementFrequency(1);
                    }
                    else
                    {
                        characterInfos.Add(c, new CharacterInfo(c, 1));
                    }
                }
            }

            return characterInfos;
        }
    }
}