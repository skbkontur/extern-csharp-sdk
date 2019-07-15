// Decompiled with JetBrains decompiler
// Type: Kontur.Api.UrnException
// Assembly: Kontur.Api, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: AA280C05-1D50-4E0C-9593-3BE7096E3298
// Assembly location: C:\Users\trubitsin\Desktop\Not My Projects\ke.api\libapi\Kontur.Api\bin\Release\Kontur.Api.dll

using System;

namespace ExternDotnetSDK.Common
{
    public class UrnException : Exception
    {
        public UrnException(string message)
            : base(message)
        {
        }

        public UrnException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}