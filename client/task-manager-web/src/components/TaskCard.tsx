import React from "react";
import { Task } from "@/types/task";
import { Input } from "./Input";
import { Textarea } from "./Textarea";
import { Button } from "./Button";
import { useDraggable } from "@dnd-kit/core";

interface TaskCardProps {
  task: Task;
  onEdit: (task: Task) => void;
  draggable?: boolean;
  // Props para edição inline
  isEditing: boolean;
  editTitle: string;
  setEditTitle: (title: string) => void;
  editDescription: string;
  setEditDescription: (description: string) => void;
  onEditSave: () => Promise<void>;
  onEditCancel: () => void;
}

export const TaskCard: React.FC<TaskCardProps> = ({
  task,
  onEdit,
  draggable = true,
  isEditing,
  editTitle,
  setEditTitle,
  editDescription,
  setEditDescription,
  onEditSave,
  onEditCancel,
}) => {
  // Hook para tornar o card arrastável
  const { attributes, listeners, setNodeRef, transform, isDragging } =
    useDraggable({
      id: task.id,
      disabled: !draggable || isEditing,
    });

  // Aplicar transformação de drag
  const style = transform
    ? {
        transform: `translate3d(${transform.x}px, ${transform.y}px, 0)`,
      }
    : undefined;

  // Função para formatar data
  const formatDate = (dateString: string) => {
    return new Date(dateString).toLocaleDateString("pt-BR", {
      day: "2-digit",
      month: "2-digit",
      year: "numeric",
      hour: "2-digit",
      minute: "2-digit",
    });
  };

  // Função para obter a cor do badge baseado no status
  const getStatusBadge = (status: number) => {
    switch (status) {
      case 1:
        return "bg-yellow-100 text-yellow-800 border-yellow-200";
      case 2:
        return "bg-blue-100 text-blue-800 border-blue-200";
      case 3:
        return "bg-green-100 text-green-800 border-green-200";
      default:
        return "bg-gray-100 text-gray-800 border-gray-200";
    }
  };

  // Se está em modo de edição, renderiza o formulário
  if (isEditing) {
    return (
      <div className="bg-white p-4 rounded-lg shadow-sm border-2 border-blue-200 transition-all duration-200">
        <div className="flex flex-col gap-3">
          <Input
            value={editTitle}
            onChange={(e) => setEditTitle(e.target.value)}
            placeholder="Título da tarefa"
            maxLength={100}
            required
            className="font-semibold"
          />
          <Textarea
            value={editDescription}
            onChange={(e) => setEditDescription(e.target.value)}
            placeholder="Descrição"
            rows={3}
            className="resize-none"
          />
          <div className="flex gap-2 mt-2">
            <Button
              onClick={onEditSave}
              className="bg-blue-500 hover:bg-blue-600 text-white px-4 py-2 text-sm rounded-md transition-colors duration-200"
            >
              Salvar
            </Button>
            <Button
              onClick={onEditCancel}
              className="bg-gray-500 hover:bg-gray-600 text-white px-4 py-2 text-sm rounded-md transition-colors duration-200"
            >
              Cancelar
            </Button>
          </div>
        </div>
      </div>
    );
  }

  // Card normal (não editando)
  return (
    <div
      ref={setNodeRef}
      style={style}
      {...listeners}
      {...attributes}
      className={`bg-white p-4 rounded-lg shadow-sm border border-gray-200 cursor-pointer hover:shadow-md transition-all duration-200 ${
        isDragging ? "opacity-50 rotate-3 scale-105" : ""
      } ${draggable ? "hover:scale-102" : ""}`}
      onClick={() => !isDragging && onEdit(task)}
    >
      {/* Header do card com status */}
      <div className="flex justify-between items-start mb-3">
        <h3 className="font-semibold text-gray-800 text-sm leading-tight flex-1 pr-2">
          {task.title}
        </h3>
        <span
          className={`px-2 py-1 text-xs font-medium rounded-full border ${getStatusBadge(
            task.status,
          )}`}
        >
          {task.status === 1
            ? "Pendente"
            : task.status === 2
              ? "Em Progresso"
              : "Concluída"}
        </span>
      </div>

      {/* Descrição */}
      {task.description && (
        <p className="text-gray-600 text-sm mb-3 line-clamp-3">
          {task.description}
        </p>
      )}

      {/* Footer com datas */}
      <div className="border-t border-gray-100 pt-3 space-y-1">
        <div className="text-xs text-gray-500 flex items-center">
          <span className="font-medium">Criado:</span>
          <span className="ml-1">{formatDate(task.createdAt)}</span>
        </div>

        {task.completedAt && (
          <div className="text-xs text-green-600 flex items-center">
            <span className="font-medium">Concluído:</span>
            <span className="ml-1">{formatDate(task.completedAt)}</span>
          </div>
        )}
      </div>

      {/* Indicador de drag */}
      {draggable && !isEditing && (
        <div className="absolute top-2 right-2 opacity-0 group-hover:opacity-100 transition-opacity duration-200">
          <div className="w-4 h-4 grid grid-cols-2 gap-0.5">
            {[...Array(4)].map((_, i) => (
              <div key={i} className="w-1 h-1 bg-gray-400 rounded-full"></div>
            ))}
          </div>
        </div>
      )}
    </div>
  );
};
