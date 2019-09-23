using System;
using System.Collections.Generic;

namespace DadataCore.Model
{
    public class SuggestResponse<T> : IDadataResponse
    {
        public IList<Suggestion<T>> suggestions;
    }
}
