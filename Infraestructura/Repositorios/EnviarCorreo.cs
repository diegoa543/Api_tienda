using MailKit.Security;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using Tienda_API.Aplicacion.Dtos;
using Tienda_API.Aplicacion.Interfaces;

namespace Tienda_API.Infraestructura.Repositorios;

public class EnviarCorreo : IEnviarCorreo
{
    private readonly ApplicationContext _context;

    public EnviarCorreo(ApplicationContext context)
    {
        _context = context;
    }

    public async Task<EnviarCorreoDTO> EnviarCorreoCliente(int clienteId, int pedidoId, decimal total)
    {
        var cliente = await _context.Usuarios.FirstOrDefaultAsync(x => x.Id == clienteId);
        var detalles = await _context.DetallesPedidos.Where(x => x.PedidoId == pedidoId).ToListAsync();
        //var mensaje = new MailMessage();
        var email = new MimeMessage();

        if (cliente != null && detalles != null)
        {
            email.From.Add(new MailboxAddress("Diego", "diegogonzalez6956@gmail.com"));
            email.To.Add(new MailboxAddress(cliente.Nombre, cliente.Email));
            email.Subject = "Confirmación de pedido";

            email.Body = new TextPart("plain")
            {
                Text = $"Estimado/a {cliente.Nombre},\n" +
                $"¡Gracias por tu compra!\n" +
                $"Este correo es para confirmar que hemos recibido tu pedido correctamente. A continuación, encontrarás los detalles de tu pedido:\n" +
                string.Join("\n", detalles.Select(x => $"- Producto: {x.Producto.Nombre}, Cantidad: {x.Cantidad}, Precio: {x.Precio}")) +
                $"\n\nTotal: {total}\n\n" +
                $"Si necesitas realizar algún cambio en tu pedido o tienes alguna pregunta, no dudes en ponerte en contacto con nuestro equipo de atención al cliente.\r\n" +
                $"Gracias por confiar en nosotros.\n" +
                $"Atentamente,\n" +
                $"Diego Gonzalez\n" +
                $"Gerente del establecimiento."
            };

            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {
                await client.ConnectAsync("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                await client.AuthenticateAsync("pruebaapi59@gmail.com", "qckx ntxu asay yydy");
                await client.SendAsync(email);
                await client.DisconnectAsync(true);
            }
            EnviarCorreoDTO enviarCorreoDTO = new EnviarCorreoDTO
            {
                ClienteId = clienteId,
                PedidoId = pedidoId,
                Total = total
            };

            return enviarCorreoDTO;
        }
        else { return null; }
    }
}
