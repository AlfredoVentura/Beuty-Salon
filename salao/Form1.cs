using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace salao
{
    public partial class Form1 : Form
    {


        //string connectionString = "Provider=SQLOLEDB.1;Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=salao;Data Source=DESKTOP-LUGRJRA";
        //SqlCommand cm = new SqlCommand();

        private PessoaDAL dal = new PessoaDAL(); // Instancia a DAL

        // Variável para armazenar o ID do cliente selecionado na tabela (chave primária)
        private int clienteSelecionadoId = 0;

        public Form1()
        {
            InitializeComponent();

            // **VERIFIQUE E CORRIJA AQUI:** Use 'CellContentClick' (o evento que você enviou).
            // Certifique-se de que não haja nenhuma linha tentando associar CellClick!
            this.dgvClientes.CellContentClick += new DataGridViewCellEventHandler(this.dgvClientes_CellContentClick);

            CarregarClientes();
        }

        private void InitializeComponent()
        {
            this.txtNome = new System.Windows.Forms.TextBox();
            this.txtTelefone = new System.Windows.Forms.TextBox();
            this.txtEndereco = new System.Windows.Forms.TextBox();
            this.btnNovo = new System.Windows.Forms.Button();
            this.btnExcluir = new System.Windows.Forms.Button();
            this.btnAtualizar = new System.Windows.Forms.Button();
            this.dgvClientes = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvClientes)).BeginInit();
            this.SuspendLayout();
            // 
            // txtNome
            // 
            this.txtNome.Location = new System.Drawing.Point(54, 35);
            this.txtNome.Name = "txtNome";
            this.txtNome.Size = new System.Drawing.Size(324, 22);
            this.txtNome.TabIndex = 0;
            this.txtNome.TextChanged += new System.EventHandler(this.txtNome_TextChanged);
            // 
            // txtTelefone
            // 
            this.txtTelefone.Location = new System.Drawing.Point(54, 97);
            this.txtTelefone.Name = "txtTelefone";
            this.txtTelefone.Size = new System.Drawing.Size(324, 22);
            this.txtTelefone.TabIndex = 1;
            this.txtTelefone.TextChanged += new System.EventHandler(this.txtTelefone_TextChanged);
            // 
            // txtEndereco
            // 
            this.txtEndereco.Location = new System.Drawing.Point(54, 163);
            this.txtEndereco.Name = "txtEndereco";
            this.txtEndereco.Size = new System.Drawing.Size(324, 22);
            this.txtEndereco.TabIndex = 2;
            this.txtEndereco.TextChanged += new System.EventHandler(this.txtEndereco_TextChanged);
            // 
            // btnNovo
            // 
            this.btnNovo.Location = new System.Drawing.Point(663, 35);
            this.btnNovo.Name = "btnNovo";
            this.btnNovo.Size = new System.Drawing.Size(175, 23);
            this.btnNovo.TabIndex = 3;
            this.btnNovo.Text = "Novo Cliente";
            this.btnNovo.UseVisualStyleBackColor = true;
            this.btnNovo.Click += new System.EventHandler(this.btnNovo_Click);
            // 
            // btnExcluir
            // 
            this.btnExcluir.Location = new System.Drawing.Point(663, 97);
            this.btnExcluir.Name = "btnExcluir";
            this.btnExcluir.Size = new System.Drawing.Size(175, 23);
            this.btnExcluir.TabIndex = 4;
            this.btnExcluir.Text = "Excluir Cliente";
            this.btnExcluir.UseVisualStyleBackColor = true;
            this.btnExcluir.Click += new System.EventHandler(this.btnExcluir_Click);
            // 
            // btnAtualizar
            // 
            this.btnAtualizar.Location = new System.Drawing.Point(663, 162);
            this.btnAtualizar.Name = "btnAtualizar";
            this.btnAtualizar.Size = new System.Drawing.Size(175, 23);
            this.btnAtualizar.TabIndex = 5;
            this.btnAtualizar.Text = "Atualizar";
            this.btnAtualizar.UseVisualStyleBackColor = true;
            this.btnAtualizar.Click += new System.EventHandler(this.btnAtualizar_Click);
            // 
            // dgvClientes
            // 
            this.dgvClientes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvClientes.Location = new System.Drawing.Point(54, 249);
            this.dgvClientes.Name = "dgvClientes";
            this.dgvClientes.RowHeadersWidth = 51;
            this.dgvClientes.RowTemplate.Height = 24;
            this.dgvClientes.Size = new System.Drawing.Size(784, 167);
            this.dgvClientes.TabIndex = 6;
            this.dgvClientes.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvClientes_CellContentClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(54, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 16);
            this.label1.TabIndex = 7;
            this.label1.Text = "Nome";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(51, 78);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 16);
            this.label2.TabIndex = 8;
            this.label2.Text = "Telefone";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(54, 144);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 16);
            this.label3.TabIndex = 9;
            this.label3.Text = "Endereço";
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(940, 458);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvClientes);
            this.Controls.Add(this.btnAtualizar);
            this.Controls.Add(this.btnExcluir);
            this.Controls.Add(this.btnNovo);
            this.Controls.Add(this.txtEndereco);
            this.Controls.Add(this.txtTelefone);
            this.Controls.Add(this.txtNome);
            this.Name = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvClientes)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        // Arquivo: Form1.cs (Dentro da classe Form1)
        // Método que Carrega/Atualiza o DataGridView
        private void CarregarClientes()
        {
            try
            {
                List<Cliente> clientes = dal.PesquisarClientes();

                // Mapeia para um formato que exibe apenas os campos relevantes
                var dadosParaExibir = clientes.Select(p => new
                {
                    p.Id,
                    p.Nome,
                    p.Telefone,
                    p.Endereco,
                    Tipo_Objeto_Csharp = p.GetType().Name
                }).ToList();

                // Atribui ao DataGridView (se o nome for 'teste', mude 'dgvClientes' para 'teste')
                this.dgvClientes.DataSource = dadosParaExibir;

                // Configuração da visualização (colunas)
                if (this.dgvClientes.Columns.Contains("Id"))
                {
                    this.dgvClientes.Columns["Id"].Visible = false;
                    this.dgvClientes.Columns["Tipo_Objeto_Csharp"].Visible = false;
                }

                // A mensagem de "0 pessoas" só aparece se a lista realmente for vazia
                // O tratamento de erro está no catch.
                if (clientes.Count > 0)
                {
                    MessageBox.Show($"Carregadas {clientes.Count} pessoas com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar dados: " + ex.Message, "Erro de Conexão/SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LimparCampos()
        {
            txtNome.Clear();
            txtTelefone.Clear();
            txtEndereco.Clear();
            clienteSelecionadoId = 0; // Reseta o ID
            txtNome.Focus();
        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void txtNome_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtTelefone_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtEndereco_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            Cliente novoCliente = new Cliente
            {
                Nome = txtNome.Text,
                Telefone = txtTelefone.Text,
                Endereco = txtEndereco.Text
            };

            int novoId = dal.InserirCliente(novoCliente);

            if (novoId > 0)
            {
                MessageBox.Show($"Cliente {novoCliente.Nome} inserido com ID: {novoId}", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CarregarClientes();
                LimparCampos();
            }
            else
            {
                MessageBox.Show("Falha na inserção do cliente. Verifique o banco de dados.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (clienteSelecionadoId <= 0)
            {
                MessageBox.Show("Selecione um cliente na tabela para excluir.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirmacao = MessageBox.Show($"Tem certeza que deseja excluir o cliente ID {clienteSelecionadoId}?",
                                                     "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirmacao == DialogResult.Yes)
            {
                // **CS1061 RESOLVIDO:** Chamada corrigida para ExcluirCliente
                int linhasAfetadas = dal.ExcluirCliente(clienteSelecionadoId);

                if (linhasAfetadas > 0)
                {
                    MessageBox.Show("Cliente excluído com sucesso.", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CarregarClientes();
                    LimparCampos();
                }
                else
                {
                    MessageBox.Show("Falha ao excluir o cliente. Verifique se ele existe.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            if (clienteSelecionadoId <= 0)
            {
                MessageBox.Show("Selecione um cliente na tabela para atualizar.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Cliente clienteAtualizado = new Cliente
            {
                Id = clienteSelecionadoId,
                Nome = txtNome.Text,
                Telefone = txtTelefone.Text,
                Endereco = txtEndereco.Text
            };

            // **CS1061 RESOLVIDO:** Chamada corrigida para AtualizarCliente
            int linhasAfetadas = dal.AtualizarCliente(clienteAtualizado);

            if (linhasAfetadas > 0)
            {
                MessageBox.Show($"Cliente ID {clienteAtualizado.Id} atualizado com sucesso.", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                CarregarClientes();
                LimparCampos();
            }
            else
            {
                MessageBox.Show("Falha ao atualizar o cliente.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void dgvClientes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Garante que uma linha válida foi clicada (não o cabeçalho)
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvClientes.Rows[e.RowIndex];

                // Armazena o ID (PK) da linha clicada
                clienteSelecionadoId = (int)row.Cells["Id"].Value;

                // Preenche os TextBoxes
                txtNome.Text = row.Cells["Nome"].Value.ToString();
                txtTelefone.Text = row.Cells["Telefone"].Value.ToString();
                txtEndereco.Text = row.Cells["Endereco"].Value.ToString();
            }
        }
    }
}
