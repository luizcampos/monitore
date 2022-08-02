using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace Monitore.Classes
{
    public class Email
    {
		public static string erro = "";
		
        //ALTERAÇÃO DE SENHA
        public static string EnviarEmail(string emailDestinatario, string assunto, string corpomsg)
        {
            try
            {
                //Cria o endereço de email do remetente
                MailAddress de = new MailAddress("MONITORE <monitoreweb@gmail.com>");

                //Cria o endereço de email do destinatário
                MailAddress para = new MailAddress(emailDestinatario);
                MailMessage mensagem = new MailMessage(de, para);
                mensagem.IsBodyHtml = true;

                //Assunto do email 
                mensagem.Subject = assunto;

                //Conteúdo do email
                mensagem.Body = corpomsg;

                //Prioridade E-mail
                mensagem.Priority = MailPriority.Normal;

                //Cria o objeto que envia o e-mail
                SmtpClient cliente = new SmtpClient();

                //Envia o email
                cliente.Send(mensagem);
                return "Enviamos um email com a nova senha para você.";
            }

            catch
            {
                return "Erro ao enviar e-mail com a nova senha.";
            }
        }
        
        //CONTATO ADM
        public static string EnviarEmailAdm(string emailDestinatario, string assunto, string corpomsg)
        {
            try
            {
                //Cria o endereço de email do remetente
                MailAddress de = new MailAddress("MONITORE <monitoreweb@gmail.com>");

                //Cria o endereço de email do destinatário
                MailAddress para = new MailAddress(emailDestinatario);
                MailMessage mensagem = new MailMessage(de, para);
                mensagem.IsBodyHtml = true;

                //Assunto do email 
                mensagem.Subject = assunto;

                //Conteúdo do email
                mensagem.Body = corpomsg;

                //Prioridade E-mail
                mensagem.Priority = MailPriority.Normal;

                //Cria o objeto que envia o e-mail
                SmtpClient cliente = new SmtpClient();

                //Envia o email
                cliente.Send(mensagem);
                return "Resposta enviada com sucesso via e-mail.";
            }

            catch (Exception e)
            {
                return "Erro ao enviar resposta via e-mail.";
				erro = e.GetBaseException().ToString();
            }
        }
    }
}