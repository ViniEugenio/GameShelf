﻿namespace GameShelf.Domain.Projections.User
{
    public class UsuarioSimplificadoProjection
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Email { get; set; }
        public bool Ativo { get; set; }
    }
}
