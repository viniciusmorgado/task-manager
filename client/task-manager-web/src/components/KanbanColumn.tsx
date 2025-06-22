import React from "react";
import { Task } from "@/types/task";
import { TaskCard } from "./TaskCard";
import { AddTaskForm } from "./AddTaskForm";

interface KanbanColumnProps {
  title: string;
  status: 1 | 2 | 3;
  tasks: Task[];
  onEdit: (task: Task) => void;
  onDropTask: (taskId: number) => void;
  onAddTask?: (title: string, description: string) => void;
  onDragOver?: (e: React.DragEvent<HTMLDivElement>) => void;
}

export const KanbanColumn: React.FC<KanbanColumnProps> = ({
  title,
  status,
  tasks,
  onEdit,
  onDropTask,
  onAddTask,
  onDragOver,
}) => (
  <div
    className={`flex flex-col rounded-lg p-4 min-w-[300px] h-full ${
      status === 1
        ? "bg-yellow-50"
        : status === 2
          ? "bg-blue-50"
          : "bg-green-50"
    }`}
    onDragOver={onDragOver}
    onDrop={(e) => {
      const taskId = Number(e.dataTransfer.getData("text/plain"));
      onDropTask(taskId);
    }}
  >
    <h2 className="font-bold text-lg mb-4">{title}</h2>
    {status === 1 && onAddTask && <AddTaskForm onAdd={onAddTask} />}
    <div className="flex flex-col gap-2 mt-2">
      {tasks.map((task) => (
        <TaskCard
          key={task.id}
          task={task}
          onEdit={onEdit}
          draggable
          onDragStart={(e) => {
            e.dataTransfer.setData("text/plain", String(task.id));
          }}
        />
      ))}
    </div>
  </div>
);
