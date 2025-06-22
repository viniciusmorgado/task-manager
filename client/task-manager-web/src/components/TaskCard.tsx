import React from "react";
import { Task } from "@/types/task";
import { Input } from "./Input";
import { Textarea } from "./Textarea";
import { Button } from "./Button";

interface TaskCardProps {
  task: Task;
  onEdit: (task: Task) => void;
  draggable?: boolean;
  onDragStart?: (e: React.DragEvent<HTMLDivElement>) => void;
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
  draggable = false,
  onDragStart,
  isEditing,
  editTitle,
  setEditTitle,
  editDescription,
  setEditDescription,
  onEditSave,
  onEditCancel,
}) => {
  if (isEditing) {
    return (
      <div className="bg-white p-4 rounded-lg shadow-sm border border-gray-200">
        <div className="flex flex-col gap-2">
          <Input
            value={editTitle}
            onChange={(e) => setEditTitle(e.target.value)}
            placeholder="Título da tarefa"
            maxLength={100}
            required
          />
          <Textarea
            value={editDescription}
            onChange={(e) => setEditDescription(e.target.value)}
            placeholder="Descrição"
            rows={3}
          />
          <div className="flex gap-2 mt-2">
            <Button
              onClick={onEditSave}
              className="bg-blue-500 hover:bg-blue-600 text-white px-3 py-1 text-sm"
            >
              Salvar
            </Button>
            <Button
              onClick={onEditCancel}
              className="bg-gray-500 hover:bg-gray-600 text-white px-3 py-1 text-sm"
            >
              Cancelar
            </Button>
          </div>
        </div>
      </div>
    );
  }

  return (
    <div
      className="bg-white p-4 rounded-lg shadow-sm border border-gray-200 cursor-pointer hover:shadow-md transition-shadow"
      draggable={draggable}
      onDragStart={onDragStart}
      onClick={() => onEdit(task)}
    >
      <h3 className="font-semibold text-gray-800 mb-2">{task.title}</h3>
      {task.description && (
        <p className="text-gray-600 text-sm mb-2">{task.description}</p>
      )}
      <div className="text-xs text-gray-500">
        Criado em: {new Date(task.createdAt).toLocaleDateString()}
      </div>
      {task.completedAt && (
        <div className="text-xs text-green-600">
          Concluído em: {new Date(task.completedAt).toLocaleDateString()}
        </div>
      )}
    </div>
  );
};
