using HiddenVilla.notify;

namespace HiddenVilla_Api.Helper
{
    public class EmailHelper : IDisposable
    {
        private IMessages Messages { get; set; }

        public EmailHelper(IMessages message)
        {
            this.Messages = message;
        }

        public void SendPaymentSuccessEmail(string email, double amount)
        {
            string emailBody = DesignEmail(
           "Payment Successful",
           $@"
        <tr>
            <td style=""font-weight: 500; color: #292929; font-family: 'Nunito', sans-serif;"">Hi {email},</td>
        </tr>
        <tr>
            <td style=""line-height: 0px;"" height=""16""></td>
        </tr>
        <tr>
            <td style=""line-height: 24px; font-family: 'Nunito', sans-serif; color: #292929;"">
                We are pleased to inform you that your payment of <b>{amount}</b> on <b>{17-12-2024}</b> has been successfully processed. 
            </td>
        </tr>
        <tr>
            <td style=""line-height: 0px;"" height=""16""></td>
        </tr>
        <tr>
            <td style=""line-height: 24px; font-family: 'Nunito', sans-serif; color: #292929;"">
                Thank you for your continued trust in {"HiddenVilla"}. If you have any questions, please feel free to contact our support team.
            </td>
        </tr>
        <tr>
            <td style=""line-height: 0px;"" height=""32""></td>
        </tr>
        <tr align=""center"">
            <td>
                <a href=""https://localhost:7169/"" 
                   style=""font-family: 'Nunito', sans-serif; padding: 12px; min-width: 216px; display: inline-block; background: #28a745; border-radius: 4px; font-size: 14px; line-height: 20px; color: #fff; text-decoration: none;"">
                    Visit Our Website
                </a>
            </td>
        </tr>
        <tr>
            <td style=""line-height: 0px;"" height=""32""></td>
        </tr>"
       );

            // Send the email
            this.SendEmail(email, "Payment Successful", emailBody);
        }

        #region Design Email

        private static string DesignEmail(
            string subject,
            string body)
        {
            if (string.IsNullOrEmpty(subject))
            {
                //throw new ArgumentException($"'{nameof(subject)}' cannot be null or empty.", nameof(subject));
            }

            // Generate response
            var response = @$"<table style=""font-family: 'Montserrat', sans-serif;"" cellpadding=""0"" cellspacing=""0"" bgcolor=""#ffffff"" width=""100%"">
	<tr>
		<td>
			<table width=""100%"" border-collapse=""collapse"" border-spacing=""0"" cellpadding=""0"" cellspacing=""0"" style=""color: #086AA1; min-width: 100%"" bgcolor=""#fff"" align=""center"">
				<tr>
					<td>
						<table border-collapse=""collapse"" border-spacing=""0"" width=""100%"" cellpadding=""0"" cellspacing=""0"" bgcolor=""#fff"" style=""min-width: 100%;"">
							<tr>
								<td style=""height: 40px""></td>
							</tr>
							<tr>
								<td>
									<table width=""552px"" cellpadding=""0"" cellspacing=""0"" align=""center"" bgcolor=""#086AA1"" style=""min-width: 600px; border-radius: 4px; padding: 20px 10px;"">
										<tr>
											<td>
												<h2 style=""width: 130px; max-width: 250px; color: #fff; margin: auto; display: table;"">HiddenVilla</h2>
											</td>
										</tr>
									</table>
								</td>
							</tr>
							<tr>
								<td style=""height: 10px""></td>
							</tr>
							<tr>
								<td>
									<table width=""552px"" cellpadding=""0"" cellspacing=""0"" align=""center"" bgcolor=""#ffffff"" style=""min-width: 600px; border-radius: 4px 4px 0px 0px;"">
										<tr>
											<td style=""height:24px;""></td>
										</tr>
										<tr>
											<td style=""margin: 0px; text-align: left; font-size: 20px; line-height: 32px; color:  #000000;;text-align: center; font-weight: 600;"">
												Contact
											</td>
										</tr>
										<tr>
											<td style=""height:12px;""></td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td>
						<table width=""100%"" cellspacing=""0"" cellpadding=""0"">
							<tr>
								<td bgcolor=""#fff"">
									<table width=""600px"" align=""center"" cellpadding=""0"" cellspacing=""0"" bgcolor=""#ffffff"" style=""border-radius: 0px 0px 4px 4px;"">
										<tr>
											<td>
												<table width=""90%"" align=""center"" cellpadding=""0"" cellspacing=""0"" bgcolor=""#ffffff"">
													<tr>
														<td style=""text-align: left;font-size: 16px; margin: 0; line-height: 24px;  color: #212121;"">
															{body}
														</td>
													</tr>
													<tr>
														<td style=""text-align: left; font-size: 16px; line-height: 24px; color: #212121;"">
															Best,
														</td>
													</tr>
													<tr>
														<td style=""text-align: left; font-size: 16px; font-weight: normal; line-height: 24px; color: #212121;"">
															HiddenVilla
														</td>
													</tr>
												</table>
											</td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td bgcolor=""#fff"" style=""height:24px""></td>
				</tr>
				<tr>
					<td bgcolor=""#fff"">
						<table width=""600px"" cellpadding=""0"" cellspacing=""0"" style=""margin: 0 auto; background: #086AA1; border-radius: 4px;"">
							<tr>
								<td style=""height:32px""></td>
							</tr>
							<tr>
								<td>
									<table width=""90%"" align=""center"" cellpadding=""0"" cellspacing=""0"">
										<tr>
											<td style=""width: 50%; flex-shrink: 0px; font-size: 16px; line-height: 24px; color: #ffffff;"">
												Need more help?</td>
											<td style=""width: 50%; flex-shrink: 0px; text-align: right;""><a href=""https://localhost:7169/"" style=""padding: 16px 30px; border-radius: 8px; display: inline-block; color: #ffffff; font-weight: 500; background:transparent; text-decoration: none; border: 2px solid #ffffff;"">Go
													to Website</a></td>
										</tr>
									</table>
								</td>
							</tr>
							<tr>
								<td style=""height:32px""></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td bgcolor=""#fff"" style=""height:56px""></td>
				</tr>
				<tr>
					<td bgcolor=""#fff"">
						<table width=""100%"" cellpadding=""0"" cellspacing=""0"" align=""center"" style="" background: transparent; border-radius: 4px;"">

							<tr>
								<td style=""text-align: center; font-size: 16px; line-height: 24px; color: #212121;"">
									© Copyright 2022 Gateiz. All Rights Reserved.
								</td>
							</tr>
							<tr>
								<td style=""height: 32px;""></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td bgcolor=""#fff"" style=""height:68px""></td>
				</tr>
			</table>
		</td>
	</tr>
</table>";

            return response;
        }

        #endregion Design Email

        #region Send Email
        private void SendEmail(string email, string subject, string body) => this.Messages.SendEmail(email, subject, body);
        #endregion Send Email


        #region Dispose
        public void Dispose() => GC.SuppressFinalize(this);

        #endregion Dispose
    }
}
