namespace APIwebEscola.Models {
    public class Aluno {

        public int id { get; set; }

        public string nome { get; set; }
        public DateTime dataNascimento { get; set; }
        public char sexo { get; set; }

        public int? totalFaltas { get; set; }
        public int? turmaid { get; set; }

        //navegation Property

        public virtual Turma Turma { get; set; }       

    }
}
