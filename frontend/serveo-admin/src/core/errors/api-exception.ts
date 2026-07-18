import type { ApiProblemDetails } from './api-problem-details';

export class ApiException extends Error {
  constructor(
    public readonly problem: ApiProblemDetails,
    public readonly status?: number,
  ) {
    super(problem.detail ?? problem.title ?? 'Unexpected error');

    this.name = 'ApiException';
  }
}
