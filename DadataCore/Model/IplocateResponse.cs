using System;

namespace DadataCore.Model
{
    public class IplocateResponse : IDadataResponse
    {
        public Suggestion<Address> location;
    }
}
