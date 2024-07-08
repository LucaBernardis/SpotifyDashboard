import { TestBed } from '@angular/core/testing';

import { TracksApiService } from './tracks-api.service';

describe('TracksApiService', () => {
  let service: TracksApiService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(TracksApiService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
