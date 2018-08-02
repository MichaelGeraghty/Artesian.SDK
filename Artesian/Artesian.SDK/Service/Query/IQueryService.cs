// Copyright (c) ARK LTD. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for
// license information. 
namespace Artesian.SDK.Service
{
    public interface IQueryService
    {
        ActualQuery CreateActual();
        VersionedQuery CreateVersioned();
        MasQuery CreateMarketAssessment();
    }
}