using Elk.Core;
using Shopia.Domain.Resource;
using System;

namespace Shopia.Domain
{
    public enum StoreType : byte
    {
        [LocalizeDescription(nameof(Strings.Instagram),typeof(Strings))]
        Instagram = 1,
        [LocalizeDescription(nameof(Strings.Telegram),typeof(Strings))]
        Telegram = 2,
        [LocalizeDescription(nameof(Strings.WhatsApp),typeof(Strings))]
        WhatsApp = 3,
    }
}
