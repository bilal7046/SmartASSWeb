using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using QRCoder;
using SmartASSWeb.Bll;
using SmartASSWeb.Bll.Services;
using SmartASSWeb.Core.Service;

namespace SmartASSWeb.Controllers
{
    public class CardsController 
        : ControllerBase
    {
        private readonly IFirebaseService _firebaseService;
        private readonly IQRCodeGeneratorService _qrCodeService;

        public CardsController(IFirebaseService firebaseService, IQRCodeGeneratorService qrCodeService)
        {
            _firebaseService = firebaseService;
            _qrCodeService = qrCodeService;
        }
        public async Task<ActionResult> Index()
        {
            var businessCards = await _firebaseService.GetCollection<BusinessCard>(FirestoreTableStore.BusinessCards, "userId", User.UserId);
            var defaultCard = businessCards.FirstOrDefault();
            ViewBag.QRCodeImage = _qrCodeService.GetQRCodeBase64(defaultCard.BusinessCodeUrl);
            return View();
        }
    }
}