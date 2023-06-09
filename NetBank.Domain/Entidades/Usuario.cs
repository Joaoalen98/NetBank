﻿namespace NetBank.Domain.Entidades
{
    public class Usuario : BaseEntidade
    {
        public string NomeCompleto { get; set; }

        public string Email { get; set; }

        public string Telefone { get; set; }

        public string CPF { get; set; }

        public DateTime DataNascimento { get; set; }

        public string Senha { get; set; }

        public IEnumerable<Conta> Contas { get; set; }

        public bool Ativo { get; set; }
    }
}
