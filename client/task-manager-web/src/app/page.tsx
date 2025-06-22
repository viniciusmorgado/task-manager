import { KanbanBoard } from "@/components/KanbanBoard";

export default function Home() {
  return (
    <main className="flex flex-col items-center justify-center min-h-screen p-8">
      <KanbanBoard />
    </main>
  );
}
