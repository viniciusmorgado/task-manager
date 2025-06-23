"use client";
import React from "react";
import { Task, TaskStatus } from "@/types/task";
import { TaskCard } from "./TaskCard";
import { getStatusLabel } from "@/utils/labels";
import {
  DragDropContext,
  Droppable,
  Draggable,
  DropResult,
} from "@hello-pangea/dnd";
import { useTaskContext } from "@/context/TaskContext";

interface KanbanBoardProps {
  tasks: Task[];
}

export const KanbanBoard: React.FC<KanbanBoardProps> = ({ tasks }) => {
  const { updateTask } = useTaskContext();

  const columns = {
    [TaskStatus.TODO]: tasks.filter((t) => t.status === TaskStatus.TODO),
    [TaskStatus.IN_PROGRESS]: tasks.filter(
      (t) => t.status === TaskStatus.IN_PROGRESS,
    ),
    [TaskStatus.COMPLETED]: tasks.filter(
      (t) => t.status === TaskStatus.COMPLETED,
    ),
  };

  const getColumnColor = (status: TaskStatus) => {
    switch (status) {
      case TaskStatus.TODO:
        return "border-orange-300 bg-orange-50";
      case TaskStatus.IN_PROGRESS:
        return "border-blue-300 bg-blue-50";
      case TaskStatus.COMPLETED:
        return "border-green-300 bg-green-50";
      default:
        return "border-gray-300 bg-gray-50";
    }
  };

  const onDragEnd = async (result: DropResult) => {
    const { source, destination, draggableId } = result;
    if (!destination) return;
    if (
      source.droppableId === destination.droppableId &&
      source.index === destination.index
    ) {
      return;
    }

    const taskId = Number(draggableId);
    const newStatus = Number(destination.droppableId) as TaskStatus;

    const currentTask = tasks.find((task) => task.id === taskId);
    if (!currentTask) return;

    await updateTask(taskId, {
      title: currentTask.title,
      description: currentTask.description,
      status: newStatus,
      createdById: currentTask.createdById,
    });
  };

  return (
    <DragDropContext onDragEnd={onDragEnd}>
      <div className="grid grid-cols-1 md:grid-cols-3 gap-6">
        {[TaskStatus.TODO, TaskStatus.IN_PROGRESS, TaskStatus.COMPLETED].map(
          (status) => (
            <Droppable droppableId={status.toString()} key={status}>
              {(provided, snapshot) => (
                <div
                  ref={provided.innerRef}
                  {...provided.droppableProps}
                  className={`border-2 rounded-lg p-4 min-h-[400px] transition-colors ${getColumnColor(status)} ${
                    snapshot.isDraggingOver ? "ring-2 ring-orange-400" : ""
                  }`}
                >
                  <div className="flex items-center justify-between mb-4">
                    <h2 className="font-bold text-lg text-gray-800">
                      {getStatusLabel(status)}
                    </h2>
                    <span
                      className={`px-2 py-1 rounded-full text-sm font-medium ${
                        status === TaskStatus.TODO
                          ? "bg-orange-100 text-orange-800"
                          : status === TaskStatus.IN_PROGRESS
                            ? "bg-blue-100 text-blue-800"
                            : "bg-green-100 text-green-800"
                      }`}
                    >
                      {columns[status].length}
                    </span>
                  </div>
                  <div className="space-y-3">
                    {columns[status].map((task, idx) => (
                      <Draggable
                        draggableId={task.id.toString()}
                        index={idx}
                        key={task.id}
                      >
                        {(provided, snapshot) => (
                          <div
                            ref={provided.innerRef}
                            {...provided.draggableProps}
                            {...provided.dragHandleProps}
                            className={`transition-shadow ${snapshot.isDragging ? "shadow-2xl" : ""}`}
                          >
                            <TaskCard task={task} />
                          </div>
                        )}
                      </Draggable>
                    ))}
                    {provided.placeholder}
                    {columns[status].length === 0 && (
                      <div className="text-center py-8 text-gray-500">
                        <p>Nenhuma tarefa nesta coluna</p>
                      </div>
                    )}
                  </div>
                </div>
              )}
            </Droppable>
          ),
        )}
      </div>
    </DragDropContext>
  );
};
