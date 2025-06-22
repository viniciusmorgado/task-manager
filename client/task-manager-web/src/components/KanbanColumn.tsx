import React from "react";
import { Task } from "@/types/task";
import { TaskCard } from "./TaskCard";
import { AddTaskForm } from "./AddTaskForm";
import { useDroppable } from "@dnd-kit/core";

interface KanbanColumnProps {
  title: string;
  status: 1 | 2 | 3;
  tasks: Task[];
  onEdit: (task: Task) => void;
  onAddTask?: (title: string, description: string) => void;
  // Props para edição inline
  editingTaskId: number | null;
  editTitle: string;
  setEditTitle: (title: string) => void;
  editDescription: string;
  setEditDescription: (description: string) => void;
  onEditSave: (task: Task) => Promise<void>;
  onEditCancel: () => void;
}

export const KanbanColumn: React.FC<KanbanColumnProps> = ({
  title,
  status,
  tasks,
  onEdit,
  onAddTask,
  editingTaskId,
  editTitle,
  setEditTitle,
  editDescription,
  setEditDescription,
  onEditSave,
  onEditCancel,
}) => {
  // Hook para tornar a coluna um local de drop
  const { setNodeRef, isOver } = useDroppable({
    id: status,
  });

  // Cores das colunas baseadas no status
  const getColumnColors = (status: number) => {
    switch (status) {
      case 1:
        return {
          bg: "bg-yellow-50",
          border: "border-yellow-200",
          header: "bg-yellow-100 text-yellow-800",
        };
      case 2:
        return {
          bg: "bg-blue-50",
          border: "border-blue-200",
          header: "bg-blue-100 text-blue-800",
        };
      case 3:
        return {
          bg: "bg-green-50",
          border: "border-green-200",
          header: "bg-green-100 text-green-800",
        };
      default:
        return {
          bg: "bg-gray-50",
          border: "border-gray-200",
          header: "bg-gray-100 text-gray-800",
        };
    }
  };

  const colors = getColumnColors(status);

  return (
    <div
      ref={setNodeRef}
      className={`flex flex-col rounded-lg border-2 p-4 min-w-[320px] h-fit min-h-[500px] transition-all duration-200 ${
        colors.bg
      } ${colors.border} ${
        isOver ? "ring-2 ring-blue-400 ring-opacity-50 scale-105" : ""
      }`}
    >
      {/* Header da coluna */}
      <div
        className={`rounded-lg px-4 py-2 mb-4 text-center font-bold text-lg ${colors.header}`}
      >
        {title}
        <span className="ml-2 text-sm font-normal opacity-75">
          ({tasks.length})
        </span>
      </div>

      {/* Formulário para adicionar tarefa (apenas na coluna "Pendente") */}
      {status === 1 && onAddTask && (
        <div className="mb-4">
          <AddTaskForm onAdd={onAddTask} />
        </div>
      )}

      {/* Lista de tarefas */}
      <div className="flex flex-col gap-3 flex-1">
        {tasks.length === 0 ? (
          <div className="text-center text-gray-500 py-8 border-2 border-dashed border-gray-300 rounded-lg">
            <p className="text-sm">
              {status === 1
                ? "Adicione uma nova tarefa acima"
                : "Arraste tarefas para cá"}
            </p>
          </div>
        ) : (
          tasks.map((task) => (
            <TaskCard
              key={task.id}
              task={task}
              onEdit={onEdit}
              draggable={editingTaskId !== task.id}
              // Props para edição inline
              isEditing={editingTaskId === task.id}
              editTitle={editTitle}
              setEditTitle={setEditTitle}
              editDescription={editDescription}
              setEditDescription={setEditDescription}
              onEditSave={() => onEditSave(task)}
              onEditCancel={onEditCancel}
            />
          ))
        )}
      </div>

      {/* Indicador visual quando está sendo usado como drop zone */}
      {isOver && (
        <div className="mt-2 p-2 border-2 border-dashed border-blue-400 rounded-lg bg-blue-50 text-center text-blue-600 text-sm">
          Solte aqui para mover a tarefa
        </div>
      )}
    </div>
  );
};
