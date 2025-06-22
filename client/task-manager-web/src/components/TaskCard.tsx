import React from "react";
import { Badge } from "./Badge";
import { Task } from "@/types/task";

interface TaskCardProps {
  task: Task;
  onEdit?: (task: Task) => void;
  onDragStart?: (event: React.DragEvent<HTMLDivElement>, task: Task) => void;
  draggable?: boolean;
}

export const TaskCard: React.FC<TaskCardProps> = ({
  task,
  onEdit,
  onDragStart,
  draggable = false,
}) => {
  return (
    <div
      className="bg-white rounded shadow p-4 mb-3 cursor-pointer border hover:border-blue-400 transition"
      draggable={draggable}
      onDragStart={onDragStart ? (e) => onDragStart(e, task) : undefined}
      tabIndex={0}
      aria-label={`Tarefa: ${task.title}`}
    >
      <div className="flex justify-between items-center mb-2">
        <h3 className="font-bold text-base text-gray-900">{task.title}</h3>
        <Badge status={task.status} />
      </div>
      {task.description && (
        <p className="text-sm text-gray-700 mb-2">{task.description}</p>
      )}
      {onEdit && (
        <button
          className="text-blue-600 text-xs underline hover:text-blue-800"
          onClick={() => onEdit(task)}
        >
          Editar
        </button>
      )}
    </div>
  );
};
