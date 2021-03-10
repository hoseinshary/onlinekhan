using AutoMapper;
using NasleGhalam.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NasleGhalam.ServiceLayer.Services
{
    /// <inheritdoc />
    /// <author>
    ///     name: امیدرضاطبیعی
    ///     date: 1399.12.20
    /// </author>
    public class PhoneVerificationService
    {
        /// <summary>
        /// ارسال کد اعتبار سنجی
        /// </summary>
        /// <param name="PhoneNumber"></param>
        /// <returns></returns>
        public async Task<long> SendVerificationCode(string PhoneNumber)
        {
            try
            {
                
                SmsPanelService.FastSendSoapClient service = new SmsPanelService.FastSendSoapClient();
                var result = await service.AutoSendCodeAsync("ramintabiee", "145863", PhoneNumber, "آنلاین خوان");
                return result.Body.AutoSendCodeResult;
                
            }
            catch (Exception exp) {
                return -1;
            }

        }
        /// <summary>
        /// بررسی کد اعتبار سنجی
        /// </summary>
        /// <param name="PhoneNumber"></param>
        /// <param name="Code"></param>
        /// <returns></returns>
        public async Task<bool> CheckVerificationCode(string PhoneNumber,string Code)
        {
            try
            {

                SmsPanelService.FastSendSoapClient service = new SmsPanelService.FastSendSoapClient();
                var result = await service.CheckSendCodeAsync("ramintabiee", "145863", PhoneNumber, Code);
                return result.Body.CheckSendCodeResult;

            }
            catch (Exception exp)
            {
                Console.WriteLine(exp.Message);
                return false;
            }

        }
    }
}
