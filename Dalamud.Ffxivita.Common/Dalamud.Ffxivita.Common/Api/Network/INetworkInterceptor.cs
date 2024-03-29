﻿namespace Dalamud.Ffxivita.Common.Api.Network
{
    public interface INetworkInterceptor : IDisposable
    {
        public void AddHandler(INetworkHandler handler);
        public void RemoveHandler(INetworkHandler handler);
    }
}
