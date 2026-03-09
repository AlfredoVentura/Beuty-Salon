using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Classe Base Abstrata: Contém apenas atributos comuns a todas as classes derivadas.
public abstract class Pessoa
{
    public int Id { get; set; }
    public string Nome { get; set; }
}

// Classe Derivada Cliente: Herda Pessoa e adiciona atributos específicos de Cliente.
public class Cliente : Pessoa
{
    // Atributos Específicos do Cliente
    public string Telefone { get; set; }
    public string Endereco { get; set; }
}

// Classe Derivada Profissional: Herda Pessoa e adiciona atributos específicos de Profissional.
public class Profissional : Pessoa
{
    // Atributos Específicos do Profissional
    public string Especialidade { get; set; }
}