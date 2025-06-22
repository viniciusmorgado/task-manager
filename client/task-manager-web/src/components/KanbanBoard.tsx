"use client";
import React, { useState } from "react";
import { useTaskContext } from "@/contexts/TaskContext";
import { KanbanColumn } from "./KanbanColumn";
import { Task } from "@/types/task";
import { labels } from "@/utils/labels";
import {
  DndContext,
  DragEndEvent,
  DragOverlay,
  DragStartEvent,
  PointerSensor,
  useSensor,
  useSensors,
} from "@dnd-kit/core";
import { TaskCard } from "./TaskCard";

const statusLabels = {
  1: labels.pending,
  2: labels.inProgress,
  3: labels.completed,
};

export const KanbanBoard: React.FC = () => {
  const { tasks, addTask, updateTask } = useTaskContext();
  const [editingTaskId, setEditingTaskId] = useState<number | null>(null);
  const [editTitle, setEditTitle] = useState("");
  const [editDescription, setEditDescription] = useState("");
  const [activeTask, setActiveTask] = useState<Task | null>(null);

  // Configuração dos sensores para drag and drop
  const sensors = useSensors(
    useSensor(PointerSensor, {
      activationConstraint: {
        distance: 8,
      },
    }),
  );

  const handleAddTask = async (title: string, description: string) => {
    const createdById = tasks[0]?.createdBy || "default-user";
    await addTask({
      title,
      description,
      status: 1,
      createdBy: createdById,
    });
  };

  const startEditTask = (task: Task) => {
    setEditingTaskId(task.id);
    setEditTitle(task.title);
    setEditDescription(task.description);
  };

  const handleEditTaskSave = async (task: Task) => {
    if (!editTitle.trim()) return;

    await updateTask(task.id, {
      title: editTitle,
      description: editDescription,
    });
    setEditingTaskId(null);
    setEditTitle("");
    setEditDescription("");
  };

  const handleEditTaskCancel = () => {
    setEditingTaskId(null);
    setEditTitle("");
    setEditDescription("");
  };

  // Função chamada quando o drag inicia
  const handleDragStart = (event: DragStartEvent) => {
    const { active } = event;
    const task = tasks.find((t) => t.id === Number(active.id));
    setActiveTask(task || null);
  };

  // Função chamada quando o drag termina
  const handleDragEnd = async (event: DragEndEvent) => {
    const { active, over } = event;
    setActiveTask(null);

    if (!over) return;

    const taskId = Number(active.id);
    const newStatus = Number(over.id) as 1 | 2 | 3;

    const task = tasks.find((t) => t.id === taskId);
    if (task && task.status !== newStatus) {
      // Atualiza o status da tarefa
      const updates: Partial<Task> = { status: newStatus };

      // Se movendo para "Concluída", adiciona a data de conclusão
      if (newStatus === 3) {
        updates.completedAt = new Date().toISOString();
      } else if (task.status === 3 && newStatus !== 3) {
        // Se saindo de "Concluída", remove a data de conclusão
        updates.completedAt = null;
      }

      await updateTask(taskId, updates);
    }
  };

  return (
    <div className="w-full max-w-7xl mx-auto p-4">
      <h1 className="text-3xl font-bold text-center mb-8 text-gray-800">
        {labels.kanbanBoard}
      </h1>

      <DndContext
        sensors={sensors}
        onDragStart={handleDragStart}
        onDragEnd={handleDragEnd}
      >
        <div className="flex gap-6 overflow-x-auto pb-4">
          {[1, 2, 3].map((status) => (
            <KanbanColumn
              key={status}
              title={statusLabels[status as 1 | 2 | 3]}
              status={status as 1 | 2 | 3}
              tasks={tasks.filter((t) => t.status === status)}
              onEdit={startEditTask}
              onAddTask={status === 1 ? handleAddTask : undefined}
              editingTaskId={editingTaskId}
              editTitle={editTitle}
              setEditTitle={setEditTitle}
              editDescription={editDescription}
              setEditDescription={setEditDescription}
              onEditSave={handleEditTaskSave}
              onEditCancel={handleEditTaskCancel}
            />
          ))}
        </div>

        {/* Overlay para mostrar o item sendo arrastado */}
        <DragOverlay>
          {activeTask ? (
            <div className="rotate-3 opacity-90">
              <TaskCard
                task={activeTask}
                onEdit={() => {}}
                isEditing={false}
                editTitle=""
                setEditTitle={() => {}}
                editDescription=""
                setEditDescription={() => {}}
                onEditSave={async () => {}}
                onEditCancel={() => {}}
              />
            </div>
          ) : null}
        </DragOverlay>
      </DndContext>
    </div>
  );
};
