using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trader.BLL.BusinessModels;
using Trader.BLL.Services.Common;
using Trader.DAL.DbModels;
using Trader.Repository.Common;

namespace Trader.BLL.Services.Extensions
{
    public static class ServicesHelpers
    {
        /// <summary>
        /// Send the resources from the wallet to another one asynchronously.
        /// </summary>
        public static async Task SendResourcesAsync
        (
            this IGenericService<ResourceWalletTransaction, ResourceWalletTransactionDto, int> service,
            ResourceWalletDto sourceWallet,
            ResourceWalletDto destWallet,
            double amountToWithdrawFromSourceWallet,
            double amountToSendToDestWallet
        )
        {
            // Check source wallet balance.

            double? sourceWalletBalance = await service.CallSfGetWalletBalanceAsync(sourceWallet.GameId, sourceWallet.ResourceId);

            if (sourceWalletBalance == null || sourceWalletBalance < amountToWithdrawFromSourceWallet)
            {
                throw new InvalidOperationException("Not enough resources on the source wallet");
            }

            // Withdraw recources from source wallet.

            var withdrawTran = new ResourceWalletTransactionDto
            {
                GameId = sourceWallet.GameId,
                ResourceId = sourceWallet.ResourceId,
                ResourceCount = -amountToWithdrawFromSourceWallet
            };
            await service.AddOrUpdateAsync(withdrawTran);

            // Add resources to the destination wallet.

            var addTran = new ResourceWalletTransactionDto
            {
                GameId = destWallet.GameId,
                ResourceId = destWallet.ResourceId,
                ResourceCount = +amountToSendToDestWallet
            };
            await service.AddOrUpdateAsync(addTran);
        }
    }
}
