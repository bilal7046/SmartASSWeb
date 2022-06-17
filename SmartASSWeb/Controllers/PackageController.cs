using System;
using System.Threading.Tasks;
using System.Web.Http;
using SmartASSWeb.Bll;
using SmartASSWeb.Bll.Services;

namespace SmartASSWeb.Controllers
{
    public class PackageController 
        : ApiController
    {
        private readonly IPackageResolverService _packageResolver;

        public PackageController(IPackageResolverService packageResolver)
        {
            _packageResolver = packageResolver;
        }

        [HttpPost]
        public async Task<IHttpActionResult> Set(string email, string package)
        {
            // ReSharper disable once InlineOutVariableDeclaration
            EnumPackage packageEnum;
            var result = Enum.TryParse<EnumPackage>(package, out packageEnum);
            if (!result)
            {
                return BadRequest($"'{package}' does not exist!");
            }

            var response = await _packageResolver.SetPackage(email, packageEnum);

            if (response.IsSuccessful)
            {
                return Ok(response);
            }

            return BadRequest(response.Message);
        }

        [HttpGet]
        public async Task<IHttpActionResult> Get(string email)
        {
            var response = await _packageResolver.GetPackageByEmail(email);

            if (response.IsSuccessful)
            {
                return Ok(response);
            }

            return BadRequest(response.Message);
        }
    }
}
