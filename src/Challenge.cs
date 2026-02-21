using System;
using System.Collections.Generic;

namespace DesignPatternChallenge
{
    // Interface comum para todas notificações
    public interface INotification
    {
        void SendOrderConfirmation(string recipient, string orderNumber);
        void SendShippingUpdate(string recipient, string trackingCode);
        void SendPaymentReminder(string recipient, decimal amount);
    }

    public class EmailNotification : INotification
    {
        public void SendOrderConfirmation(string recipient, string orderNumber)
        {
            Console.WriteLine($"Email para {recipient}");
            Console.WriteLine($"Assunto: Confirmação de Pedido");
            Console.WriteLine($"Mensagem: Seu pedido {orderNumber} foi confirmado!");
        }

        public void SendShippingUpdate(string recipient, string trackingCode)
        {
            Console.WriteLine($"Email para {recipient}");
            Console.WriteLine($"Assunto: Pedido Enviado");
            Console.WriteLine($"Mensagem: Rastreamento {trackingCode}");
        }

        public void SendPaymentReminder(string recipient, decimal amount)
        {
            Console.WriteLine($"Email para {recipient}");
            Console.WriteLine($"Assunto: Lembrete de Pagamento");
            Console.WriteLine($"Mensagem: Pagamento pendente R$ {amount:N2}");
        }
    }

    public class SmsNotification : INotification
    {
        public void SendOrderConfirmation(string recipient, string orderNumber)
        {
            Console.WriteLine($"SMS para {recipient}");
            Console.WriteLine($"Mensagem: Pedido {orderNumber} confirmado!");
        }

        public void SendShippingUpdate(string recipient, string trackingCode)
        {
            Console.WriteLine($"SMS para {recipient}");
            Console.WriteLine($"Mensagem: Rastreamento {trackingCode}");
        }

        public void SendPaymentReminder(string recipient, decimal amount)
        {
            Console.WriteLine($"SMS para {recipient}");
            Console.WriteLine($"Mensagem: Pagamento pendente R$ {amount:N2}");
        }
    }

    public class PushNotification : INotification
    {
        public void SendOrderConfirmation(string recipient, string orderNumber)
        {
            Console.WriteLine($"Push para {recipient}");
            Console.WriteLine($"Título: Pedido Confirmado");
            Console.WriteLine($"Mensagem: Pedido {orderNumber} confirmado!");
        }

        public void SendShippingUpdate(string recipient, string trackingCode)
        {
            Console.WriteLine($"Push para {recipient}");
            Console.WriteLine($"Título: Pedido Enviado");
            Console.WriteLine($"Mensagem: Rastreamento {trackingCode}");
        }

        public void SendPaymentReminder(string recipient, decimal amount)
        {
            Console.WriteLine($"Push para {recipient}");
            Console.WriteLine($"Título: Pagamento Pendente");
            Console.WriteLine($"Mensagem: Valor R$ {amount:N2}");
        }
    }

    public class WhatsAppNotification : INotification
    {
        public void SendOrderConfirmation(string recipient, string orderNumber)
        {
            Console.WriteLine($"WhatsApp para {recipient}");
            Console.WriteLine($"Mensagem: Pedido {orderNumber} confirmado!");
        }

        public void SendShippingUpdate(string recipient, string trackingCode)
        {
            Console.WriteLine($" WhatsApp para {recipient}");
            Console.WriteLine($"Mensagem: Rastreamento {trackingCode}");
        }

        public void SendPaymentReminder(string recipient, decimal amount)
        {
            Console.WriteLine($"WhatsApp para {recipient}");
            Console.WriteLine($"Mensagem: Pagamento pendente R$ {amount:N2}");
        }
    }

    public class NotificationFactory
    {
        private static readonly Dictionary<string, Func<INotification>> _notifications =
            new Dictionary<string, Func<INotification>>(StringComparer.OrdinalIgnoreCase)
        {
            { "email", () => new EmailNotification() },
            { "sms", () => new SmsNotification() },
            { "push", () => new PushNotification() },
            { "whatsapp", () => new WhatsAppNotification() }
        };

        public static INotification Create(string type)
        {
            if (_notifications.ContainsKey(type))
                return _notifications[type]();

            throw new ArgumentException($"Tipo de notificação '{type}' não suportado");
        }

        // Permite registrar novos tipos sem alterar o código existente
        public static void Register(string type, Func<INotification> creator)
        {
            _notifications[type] = creator;
        }
    }

    public class NotificationManager
    {
        public void SendOrderConfirmation(string recipient, string orderNumber, string type)
        {
            var notification = NotificationFactory.Create(type);
            notification.SendOrderConfirmation(recipient, orderNumber);
        }

        public void SendShippingUpdate(string recipient, string trackingCode, string type)
        {
            var notification = NotificationFactory.Create(type);
            notification.SendShippingUpdate(recipient, trackingCode);
        }

        public void SendPaymentReminder(string recipient, decimal amount, string type)
        {
            var notification = NotificationFactory.Create(type);
            notification.SendPaymentReminder(recipient, amount);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Sistema de Notificações (Refatorado) ===\n");

            var manager = new NotificationManager();

            manager.SendOrderConfirmation("cliente@email.com", "12345", "email");
            Console.WriteLine();

            manager.SendOrderConfirmation("+5511999999999", "12346", "sms");
            Console.WriteLine();

            manager.SendShippingUpdate("device-token-abc123", "BR123456789", "push");
            Console.WriteLine();

            manager.SendPaymentReminder("+5511888888888", 150.00m, "whatsapp");

            Console.ReadLine();
        }
    }
}