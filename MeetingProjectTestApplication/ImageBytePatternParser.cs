using Markdig.Parsers;
using Markdig.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetingProjectTestApplication
{
    public class ImageBytePatternParser : BlockParser
    {
        public ImageBytePatternParser()
        {
            OpeningCharacters = new[] { '%' };

            // Add other settings for your custom parser as needed
        }

        public override BlockState TryOpen(BlockProcessor processor)
        {
            // Add custom parsing logic to identify your pattern here
            throw new System.NotImplementedException();
        }

        public override BlockState TryContinue(BlockProcessor processor, Block block)
        {
            // Add custom parsing logic to continue processing your pattern here
            throw new System.NotImplementedException();
        }
    }
}
