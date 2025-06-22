"use client";
import { TaskProvider } from "@/contexts/TaskContext";
import { KanbanBoard } from "@/components/KanbanBoard";
import { TaskListView } from "@/components/TaskListView";
import { Tabs } from "@/components/Tabs";
import { labels } from "@/utils/labels";
import { useState } from "react";

export default function Home() {
  const [activeTab, setActiveTab] = useState<"kanban" | "list">("kanban");

  const tabs = [
    {
      id: "kanban" as const,
      label: labels.kanbanBoard,
      icon: "📋",
    },
    {
      id: "list" as const,
      label: labels.taskList,
      icon: "📝",
    },
  ];

  return (
    <TaskProvider>
      <main className="min-h-screen bg-gray-50">
        <div className="container mx-auto px-4 py-8">
          {/* Header */}
          <div className="text-center mb-8">
            <h1 className="text-4xl font-bold text-gray-800 mb-2">
              Gerenciador de Tarefas
            </h1>
            <p className="text-gray-600">
              Organize suas tarefas de forma eficiente
            </p>
          </div>

          {/* Tabs */}
          <div className="mb-8">
            <Tabs
              tabs={tabs}
              activeTab={activeTab}
              onTabChange={setActiveTab}
            />
          </div>

          {/* Content */}
          <div className="bg-white rounded-lg shadow-sm border border-gray-200 p-6">
            {activeTab === "kanban" ? <KanbanBoard /> : <TaskListView />}
          </div>
        </div>
      </main>
    </TaskProvider>
  );
}
