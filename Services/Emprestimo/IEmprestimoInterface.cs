﻿using LojaLivros.Dtos.Response;
using LojaLivros.Models;

namespace LojaLivros.Services.Emprestimo
{
    public interface IEmprestimoInterface
    {
        Task<ServiceResponse<EmprestimoModel>> Emprestar(int livroId);
        Task<List<EmprestimoModel>> BuscarEmprestimos(UsuarioModel usuario);
        Task<EmprestimoModel> Devolver(int idEmprestimo);
        Task<List<LivroModel>> BuscarEmprestimosFiltro(UsuarioModel usuario, string pesquisar);
        Task<List<EmprestimoModel>> BuscarEmprestimosGeral(string tipo = null);
    }
}
