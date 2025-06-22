import React from "react";

interface Tab {
  label: string;
  value: string;
}

interface TabsProps {
  tabs: Tab[];
  activeTab: string;
  onTabChange: (value: string) => void;
}

export const Tabs: React.FC<TabsProps> = ({ tabs, activeTab, onTabChange }) => (
  <div className="flex border-b mb-4">
    {tabs.map((tab) => (
      <button
        key={tab.value}
        className={`px-4 py-2 font-semibold transition-colors ${
          activeTab === tab.value
            ? "border-b-2 border-blue-600 text-blue-600"
            : "text-gray-600 hover:text-blue-600"
        }`}
        onClick={() => onTabChange(tab.value)}
        type="button"
      >
        {tab.label}
      </button>
    ))}
  </div>
);
