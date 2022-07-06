

namespace APIwebEscola.Models {
    public class Turma {
        public int id { get; set; }
        public string nome { get; set; }
        public bool? ativo { get; set; }

        //navegation Property

        // Aluno tem que ser do tipo enumerable pra poder
        // fazer o relacionamento pela navegation string do context

    
        internal virtual List<Aluno>? Alunos { get;set;}
      
    }
}
