using System.Collections.Generic;

namespace Dadata.Model
{
    public class SuggestResponse<T> : IDadataResponse
    {
        public IList<Suggestion<T>> suggestions;
    }
}
