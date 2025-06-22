import React from "react";

export const Textarea = (
  props: React.TextareaHTMLAttributes<HTMLTextAreaElement>,
) => (
  <textarea
    className="border rounded px-3 py-2 w-full focus:outline-none focus:ring-2 focus:ring-blue-400 resize-y"
    {...props}
  />
);
