using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiViaCep.Services
{

    public class MetodosGerais
    {
        public static bool _isUpdatingCep;

        public void FormataCep(object sender, TextChangedEventArgs e)
        {
            if (_isUpdatingCep)
                return;

            _isUpdatingCep = true;

            var entry = (Entry)sender;

            // Salva posição do cursor antes da mudança
            int oldCursorPosition = entry.CursorPosition;

            // Remove tudo que não for número
            string digits = new string(e.NewTextValue?.Where(char.IsDigit).ToArray());

            // Aplica a máscara 00000-000
            if (digits.Length > 5)
                digits = digits.Insert(5, "-");

            if (digits.Length > 9)
                digits = digits.Substring(0, 9);

            // Atualiza texto somente se for diferente
            if (entry.Text != digits)
                entry.Text = digits;

            // Reposiciona cursor no fim ou no local adequado
            entry.CursorPosition = digits.Length;

            _isUpdatingCep = false;
        }
    }
}
