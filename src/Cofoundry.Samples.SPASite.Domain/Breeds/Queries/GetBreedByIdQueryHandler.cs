﻿using Cofoundry.Core;
using Cofoundry.Domain;
using Cofoundry.Domain.CQS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cofoundry.Samples.SPASite.Domain
{
    public class GetBreedByIdQueryHandler
        : IAsyncQueryHandler<GetBreedByIdQuery, Breed>
        , IIgnorePermissionCheckHandler
    {
        private readonly ICustomEntityRepository _customEntityRepository;

        public GetBreedByIdQueryHandler(
            ICustomEntityRepository customEntityRepository
            )
        {
            _customEntityRepository = customEntityRepository;
        }

        public async Task<Breed> ExecuteAsync(GetBreedByIdQuery query, IExecutionContext executionContext)
        {
            var customEntityQuery = new GetCustomEntityRenderSummaryByIdQuery() { CustomEntityId = query.BreedId };
            var customEntity = await _customEntityRepository.GetCustomEntityRenderSummaryByIdAsync(customEntityQuery); ;
            if (customEntity == null) return null;

            return MapBreed(customEntity);
        }

        private Breed MapBreed(CustomEntityRenderSummary customEntity)
        {
            var breed = new Breed();

            breed.BreedId = customEntity.CustomEntityId;
            breed.Title = customEntity.Title;

            return breed;
        }
    }
}
