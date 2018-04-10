using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Client;
using SampleActor.Interfaces;

namespace SampleWebService.Controllers
{
    [Route("api/[controller]")]
    public class ReverseController : Controller
    {
        [HttpGet("{val}")]
        public async Task<IActionResult> Get(string val)
        {
            //ActorId actorId = ActorId.CreateRandom();
            ActorId actorId = new ActorId("myonlyactor");
            var actor = ActorProxy.Create<ISampleActor>(actorId, new Uri("fabric:/SampleServiceFabricApp/SampleActorService"));

            var cancellationToken = new System.Threading.CancellationToken();
            var result = await actor.Reverse(val, cancellationToken);
            return Ok(result);
        }
    }
}