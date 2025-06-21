using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManager.Domain.Enumerators;
using TaskManager.Domain.ValueObjects;
using TaskEntity = TaskManager.Domain.Entities.Task;

namespace TaskManager.Infrastructure.Mappings;

public class TaskMap : IEntityTypeConfiguration<TaskEntity>
{
    public void Configure(EntityTypeBuilder<TaskEntity> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Title)
                    .HasConversion(
                        v => v.Value,
                        v => new Title(v))
                    .IsRequired()
                    .HasMaxLength(100);

        builder.Property(x => x.Description)
            .HasConversion(
                v => v.Value,
                v => new Description(v))
            .HasMaxLength(1000);

        builder.HasData(
            new TaskEntity { Id = 1, Title = "Organizar vitrine de verão", Description = "Montar a vitrine com as novas peças da coleção de verão, destacando promoções de bermudas e camisetas.", Status = Status.Pending, CreatedById = "944cc574-7936-4e3f-8b59-56c4af209d6f", CreatedAt = new DateTime(2025, 6, 20, 22, 32, 35, DateTimeKind.Utc) },
            new TaskEntity { Id = 2, Title = "Conferir estoque de tênis", Description = "Verificar a quantidade de tênis esportivos no estoque e atualizar o sistema caso haja divergências.", Status = Status.Pending, CreatedById = "944cc574-7936-4e3f-8b59-56c4af209d6f", CreatedAt = new DateTime(2025, 6, 20, 22, 32, 35, DateTimeKind.Utc) },
            new TaskEntity { Id = 3, Title = "Atender cliente na seção feminina", Description = "Auxiliar cliente na escolha de vestidos para festa e sugerir acessórios combinando.", Status = Status.Pending, CreatedById = "944cc574-7936-4e3f-8b59-56c4af209d6f", CreatedAt = new DateTime(2025, 6, 20, 22, 32, 35, DateTimeKind.Utc) },
            new TaskEntity { Id = 4, Title = "Cadastrar novos produtos", Description = "Inserir no sistema as novas jaquetas recebidas do fornecedor, incluindo fotos e preços.", Status = Status.Pending, CreatedById = "944cc574-7936-4e3f-8b59-56c4af209d6f", CreatedAt = new DateTime(2025, 6, 20, 22, 32, 35, DateTimeKind.Utc) },
            new TaskEntity { Id = 5, Title = "Realizar troca de mercadoria", Description = "Efetuar a troca de uma calça jeans conforme solicitação do cliente, seguindo o procedimento da loja.", Status = Status.Pending, CreatedById = "944cc574-7936-4e3f-8b59-56c4af209d6f", CreatedAt = new DateTime(2025, 6, 20, 22, 32, 35, DateTimeKind.Utc) },
            new TaskEntity { Id = 6, Title = "Limpar provadores", Description = "Organizar e higienizar os provadores ao final do expediente.", Status = Status.Pending, CreatedById = "944cc574-7936-4e3f-8b59-56c4af209d6f", CreatedAt = new DateTime(2025, 6, 20, 22, 32, 35, DateTimeKind.Utc) },
            new TaskEntity { Id = 7, Title = "Enviar relatório de vendas", Description = "Gerar e enviar o relatório diário de vendas para o gerente até as 18h.", Status = Status.Pending, CreatedById = "944cc574-7936-4e3f-8b59-56c4af209d6f", CreatedAt = new DateTime(2025, 6, 20, 22, 32, 35, DateTimeKind.Utc) },
            new TaskEntity { Id = 8, Title = "Repor camisetas na arara", Description = "Verificar a arara de camisetas básicas e repor os tamanhos que estiverem em falta.", Status = Status.Pending, CreatedById = "944cc574-7936-4e3f-8b59-56c4af209d6f", CreatedAt = new DateTime(2025, 6, 20, 22, 32, 35, DateTimeKind.Utc) },
            new TaskEntity { Id = 9, Title = "Acompanhar entrega de fornecedor", Description = "Receber e conferir a entrega de sapatos do fornecedor, checando quantidades e modelos.", Status = Status.Pending, CreatedById = "944cc574-7936-4e3f-8b59-56c4af209d6f", CreatedAt = new DateTime(2025, 6, 20, 22, 32, 35, DateTimeKind.Utc) },
            new TaskEntity { Id = 10, Title = "Montar kit presente", Description = "Preparar um kit presente com camiseta, cinto e carteira para cliente que solicitou embalagem especial.", Status = Status.Pending, CreatedById = "944cc574-7936-4e3f-8b59-56c4af209d6f", CreatedAt = new DateTime(2025, 6, 20, 22, 32, 35, DateTimeKind.Utc) },
            new TaskEntity { Id = 11, Title = "Atualizar preços de liquidação", Description = "Alterar os preços das peças em liquidação no sistema e nas etiquetas da loja.", Status = Status.Pending, CreatedById = "944cc574-7936-4e3f-8b59-56c4af209d6f", CreatedAt = new DateTime(2025, 6, 20, 22, 32, 35, DateTimeKind.Utc) },
            new TaskEntity { Id = 12, Title = "Atender cliente no caixa", Description = "Finalizar a compra de um cliente, oferecendo opções de parcelamento e embalagem para presente.", Status = Status.Pending, CreatedById = "944cc574-7936-4e3f-8b59-56c4af209d6f", CreatedAt = new DateTime(2025, 6, 20, 22, 32, 35, DateTimeKind.Utc) },
            new TaskEntity { Id = 13, Title = "Separar pedidos do e-commerce", Description = "Selecionar e embalar os produtos vendidos pelo site para envio via transportadora.", Status = Status.Pending, CreatedById = "944cc574-7936-4e3f-8b59-56c4af209d6f", CreatedAt = new DateTime(2025, 6, 20, 22, 32, 35, DateTimeKind.Utc) },
            new TaskEntity { Id = 14, Title = "Treinar novo colaborador", Description = "Apresentar a loja, explicar procedimentos de atendimento e demonstrar o sistema de vendas ao novo funcionário.", Status = Status.Pending, CreatedById = "944cc574-7936-4e3f-8b59-56c4af209d6f", CreatedAt = new DateTime(2025, 6, 20, 22, 32, 35, DateTimeKind.Utc) },
            new TaskEntity { Id = 15, Title = "Fazer inventário semanal", Description = "Realizar contagem dos produtos em estoque e registrar eventuais diferenças para conferência.", Status = Status.Pending, CreatedById = "944cc574-7936-4e3f-8b59-56c4af209d6f", CreatedAt = new DateTime(2025, 6, 20, 22, 32, 35, DateTimeKind.Utc) }
        );
    }
}
